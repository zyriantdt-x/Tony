using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using Tony.Listener.Encoding;
using Tony.Listener.Handlers;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Tcp;
internal class TonyConnectionHandler : ConnectionHandler {
    private readonly ILogger<TonyConnectionHandler> logger;

    private readonly TonyClientService client_service;
    private readonly ParserRegistry parsers;
    private readonly HandlerRegistry handlers;

    public TonyConnectionHandler( ILogger<TonyConnectionHandler> logger, TonyClientService client_service, ParserRegistry parsers, HandlerRegistry handlers ) {
        this.logger = logger;

        this.client_service = client_service;
        this.parsers = parsers;
        this.handlers = handlers;
    }

    public async override Task OnConnectedAsync( ConnectionContext connection ) {
        this.logger.LogInformation( $"Client connected: {connection.RemoteEndPoint}" );

        PipeReader reader = connection.Transport.Input;
        PipeWriter writer = connection.Transport.Output;

        TonyClient client = new( writer );
        this.client_service.RegisterClient( client );

        List<byte> hello = Base64Encoding.Encode( 0, 2 ).ToList();
        hello.Add( 1 );
        await writer.WriteAsync( new( hello.ToArray() ) );

        while( true ) {
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;

            if( result.IsCompleted )
                break;

            Message message = new( buffer.ToArray() );
            this.logger.LogInformation( $"[{client.Uuid}][i]: {message.ToString()}" );

            // this should probably be an exception - might change that in prod
            IParser? parser = this.parsers.GetParser( message.Header );
            if( parser is null ) {
                this.logger.LogWarning( $"Failed to find parser for header {message.Header}" );
                goto consume_buf;
            }

            Handlers.IHandler? handler = this.handlers.GetHandler( message.Header );
            if( handler is null ) {
                this.logger.LogWarning( $"Failed to find handler for header {message.Header}" );
                goto consume_buf;
            }

            object parsed_message = parser.Parse( message );

            await handler.Handle( client, parsed_message );
            this.logger.LogInformation( $"Handler {handler.GetType().Name} completed." );

consume_buf:
            // Mark buffer as consumed
            reader.AdvanceTo( buffer.End );
        }

        this.logger.LogInformation( $"Client disconnected: {connection.RemoteEndPoint}" );
        this.client_service.DeregisterClient( client );
    }
}