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

    public TcpService( ILogger<TcpService> logger, IOptions<ServerOptions> options, TcpClientHandler handler ) {
        this.logger = logger;
        this.options = options.Value;
        this.handler = handler;

        this.tcp_listener = this.CreateTcpListener();
    }

    private TcpListener CreateTcpListener() {
        IPAddress address = IPAddress.Parse( this.options.ListeningAddress );
        return new TcpListener( address, this.options.Port );
    }

    public async Task StartAsync( CancellationToken cancellation_token ) {
        this.tcp_listener.Start();
        this.logger.LogInformation( "Server has started listening." );

        while( !cancellation_token.IsCancellationRequested ) {
            TcpClient client = await this.tcp_listener.AcceptTcpClientAsync();

            _ = Task.Run( () => this.handler.HandleClient( client, cancellation_token ) );

            this.logger.LogInformation( "client connected" );
        }

        this.tcp_listener.Stop();
    }

    public Task StopAsync( CancellationToken cancellation_token ) {
        _ = 1 + 1;
        this.tcp_listener.Stop();

        return Task.CompletedTask;
    }
}