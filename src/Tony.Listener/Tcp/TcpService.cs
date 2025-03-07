using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Net;
using Tony.Listener.Options;

namespace Tony.Listener.Tcp;
internal class TcpService : IHostedService {
    private readonly ILogger<TcpService> logger;
    private readonly ServerOptions options;
    private readonly TcpClientHandler handler;

    private readonly TcpListener tcp_listener;

    private CancellationTokenSource? internal_cts;

    public TcpService( ILogger<TcpService> logger, IOptions<ServerOptions> options, TcpClientHandler handler ) {
        this.logger = logger;
        this.options = options.Value;
        this.handler = handler;
        this.tcp_listener = new TcpListener( IPAddress.Parse( this.options.ListeningAddress ), this.options.Port );
    }

    public Task StartAsync( CancellationToken cancellation_token ) {
        this.logger.LogInformation( "Starting TCP Server on {Address}:{Port}", this.options.ListeningAddress, this.options.Port );
        this.tcp_listener.Start( 50 );

        this.internal_cts = CancellationTokenSource.CreateLinkedTokenSource( cancellation_token );

        _ = this.AcceptClientsAsync( this.internal_cts.Token ); // Run listener in background
        return Task.CompletedTask;
    }

    private async Task AcceptClientsAsync( CancellationToken cancellation_token ) {
        try {
            while( !cancellation_token.IsCancellationRequested ) {
                TcpClient client = await this.tcp_listener.AcceptTcpClientAsync();
                client.ReceiveBufferSize = 65535;
                client.NoDelay = true;
                //client.Client.Blocking = false;
                this.logger.LogInformation( "Client connected from {Address}", (( IPEndPoint )client.Client.RemoteEndPoint)?.ToString() );

                _ = this.handler.HandleClient( client, cancellation_token );
            }
        } catch( Exception ex ) when( ex is not OperationCanceledException ) {
            this.logger.LogError( ex, "Error in AcceptClientsAsync" );
        }
    }

    public Task StopAsync( CancellationToken cancellationToken ) {
        this.logger.LogInformation( "Stopping TCP Server..." );
        this.internal_cts.Cancel();
        this.tcp_listener.Stop();
        this.logger.LogInformation( "TCP Server stopped." );
        return Task.CompletedTask;
    }
}