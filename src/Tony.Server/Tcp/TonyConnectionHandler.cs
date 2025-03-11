using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.IO.Pipelines;
using Tony.Sdk.Clients;
using Tony.Sdk.Encoding;
using Tony.Sdk.Revisions;
using Tony.Server.Tcp.Clients;
using Tony.Server.Tcp.Registries;

namespace Tony.Server.Tcp;
internal class TonyConnectionHandler : ConnectionHandler {
    private readonly ILogger<TonyConnectionHandler> logger;

    private readonly ITonyClientService client_service;
    private readonly ParserRegistry parsers;
    private readonly HandlerRegistry handlers;

    public TonyConnectionHandler( ILogger<TonyConnectionHandler> logger, ITonyClientService client_service, ParserRegistry parsers, HandlerRegistry handlers ) {
        this.logger = logger;

        this.client_service = client_service;
        this.parsers = parsers;
        this.handlers = handlers;
    }

    public async override Task OnConnectedAsync( ConnectionContext connection ) {
        this.logger.LogInformation( $"Client connected: {connection.RemoteEndPoint}" );

        PipeReader pipe_reader = connection.Transport.Input;
        PipeWriter pipe_writer = connection.Transport.Output;

        TonyClient client = new( pipe_writer );
        this.client_service.RegisterClient( client );

        // Send initial handshake message
        List<byte> hello = Base64Encoding.Encode( 0, 2 ).ToList();
        hello.Add( 1 );
        await pipe_writer.WriteAsync( new ReadOnlyMemory<byte>( hello.ToArray() ) );

        while( true ) {
            ReadResult read_result = await pipe_reader.ReadAsync();
            ReadOnlySequence<byte> buffer = read_result.Buffer;

            if( read_result.IsCompleted && buffer.IsEmpty )
                break;

            var reader_sequence = new SequenceReader<byte>( buffer );

            while( this.TryParsePacket( ref reader_sequence, out byte[] message_buffer ) ) {
                this.ProcessPacket( client, message_buffer );
            }

            pipe_reader.AdvanceTo( reader_sequence.Position );
        }

        this.logger.LogInformation( $"Client disconnected: {connection.RemoteEndPoint}" );
        this.client_service.DeregisterClient( client );
    }

    private bool TryParsePacket( ref SequenceReader<byte> reader, out byte[] message_buffer ) {
        message_buffer = Array.Empty<byte>();

        if( reader.Remaining < 5 ) // Minimum size: 3 bytes (length) + 2 bytes (header)
            return false;

        // Read and decode the 3-byte packet length
        Span<byte> length_bytes = stackalloc byte[ 3 ];
        reader.TryCopyTo( length_bytes );
        int packet_length = Base64Encoding.Decode( length_bytes.ToArray() );
        reader.Advance( 3 );

        if( packet_length < 2 || reader.Remaining < packet_length ) // Ensure enough bytes remain
        {
            this.logger.LogWarning( $"Invalid packet length: {packet_length}" );
            return false;
        }

        // Read the full message (header included as first two bytes)
        message_buffer = new byte[ packet_length ];
        reader.TryCopyTo( message_buffer );
        reader.Advance( packet_length );

        return true;
    }

    private async void ProcessPacket( TonyClient client, byte[] message_buffer ) {
        ClientMessage message = new( message_buffer );
        this.logger.LogInformation( $"[{client.Uuid}][i]: {message}" );

        IParser? parser = this.parsers.GetParser( message.Header );
        if( parser is null ) {
            this.logger.LogWarning( $"No parser found for header {message.Header}" );
        }

        IHandler? handler = this.handlers.GetHandler( message.Header );
        if( handler is null ) {
            this.logger.LogWarning( $"No handler found for header {message.Header}" );
            return;
        }

        object parsed_message = parser?.Parse( message ) ?? new();
        await handler.Handle( client, parsed_message );
        this.logger.LogInformation( $"Handler {handler.GetType().Name} completed." );
    }
}