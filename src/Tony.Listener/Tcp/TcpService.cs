using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;

namespace Tony.Listener.Tcp;
internal class TcpService : IHostedService {
    private readonly ILogger<TcpService> logger;
    private readonly ServerOptions options;

    private readonly IEventLoopGroup master_group;
    private readonly IEventLoopGroup slave_group;

    private readonly ServerBootstrap bootstrap;

    public TcpService( ILogger<TcpService> logger, IOptions<ServerOptions> options ) {
        this.logger = logger;
        this.options = options;

    }

    public Task StartAsync( CancellationToken cancellation_token ) {
    
    }

    public Task StopAsync( CancellationToken cancellation_token ) {
    
    }
}
