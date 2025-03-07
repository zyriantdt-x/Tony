using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Tony.Listener.Encoding;
using Tony.Listener.Handlers;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Tcp;
internal class TcpClientHandler {
    private readonly ILogger<TcpClientHandler> logger;
    private readonly TonyClientService client_service;
    private readonly ParserRegistry parser_registry;
    private readonly HandlerRegistry handler_registry;

    public TcpClientHandler( ILogger<TcpClientHandler> logger, TonyClientService client_service, ParserRegistry parser_registry, HandlerRegistry handler_registry ) {
        this.logger = logger;
        this.client_service = client_service;

        this.parser_registry = parser_registry;
        this.handler_registry = handler_registry;
    }

    public async Task HandleClient( TcpClient client, CancellationToken cancellation_token ) {
        using NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[ 1024 ];

        TonyClient tony_client = new() { TcpClient = client };
        this.client_service.RegisterClient( tony_client );

        try {
            // we might make this prettier later
            List<byte> hello = Base64Encoding.Encode( 0, 2 ).ToList();
            hello.Add( 1 );
            await stream.WriteAsync( new(hello.ToArray()) );

            while( !cancellation_token.IsCancellationRequested ) {
                int bytesRead = await stream.ReadAsync( buffer, 0, buffer.Length, cancellation_token );
                if( bytesRead == 0 ) break; // Client disconnected

                Message message = new( buffer );
                _ = this.HandleClientMessage( tony_client, message );
            }
        } catch( Exception ex ) when( ex is not OperationCanceledException ) {
            this.logger.LogError( $"Client error: {ex}" );
        } finally {
            client.Close();
            this.client_service.DeregisterClient( tony_client );
            this.logger.LogInformation( "Client disconnected." );
        }
    }

    private async Task HandleClientMessage( TonyClient client, Message message ) {
        this.logger.LogInformation( $"[{client.Uuid}][i]: {message.ToString()}" );

        // this should probably be an exception - might change that in prod
        IParser? parser = this.parser_registry.GetParser( message.Header );
        if( parser is null ) {
            this.logger.LogWarning( $"Failed to find parser for header {message.Header}" );
            return;
        }

        Handlers.IHandler? handler = this.handler_registry.GetHandler( message.Header );
        if( handler is null ) {
            this.logger.LogWarning( $"Failed to find handler for header {message.Header}" );
            return;
        }

        object parsed_message = parser.Parse( message );

        await handler.Handle(client, parsed_message);
        this.logger.LogInformation( $"Handler {handler.GetType().Name} completed." );
    }
}
