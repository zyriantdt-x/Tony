using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Tony.Sdk.Options;

namespace Tony.Server.Tcp;
internal class TcpService : IHostedService {
    private readonly ILogger<TcpService> logger;
    private readonly ServerOptions options;
    private readonly ChannelInitializer<IChannel> initialiser;

    private readonly IEventLoopGroup master_group;
    private readonly IEventLoopGroup slave_group;

    private readonly ServerBootstrap bootstrap;

    public TcpService( ILogger<TcpService> logger, IOptions<ServerOptions> options, ChannelInitializer<IChannel> initialiser ) {
        this.logger = logger;
        this.options = options.Value;
        this.initialiser = initialiser;

        this.master_group = new MultithreadEventLoopGroup( 10 );
        this.slave_group = new MultithreadEventLoopGroup( 10 );

        this.bootstrap = new();
        this.bootstrap
            .Group( this.master_group, this.slave_group )
            .Channel<TcpServerSocketChannel>()
            .ChildOption( ChannelOption.TcpNodelay, true )
            .ChildOption( ChannelOption.SoKeepalive, true )
            .ChildOption( ChannelOption.SoReuseaddr, true )
            .ChildOption( ChannelOption.SoRcvbuf, 1024 )
            .ChildOption( ChannelOption.SoBacklog, 50 )
            .ChildOption( ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator( 1024 ) )
            .ChildOption( ChannelOption.Allocator, UnpooledByteBufferAllocator.Default )
            .ChildHandler( this.initialiser );

    }

    public async Task StartAsync( CancellationToken cancellation_token ) {
        await this.bootstrap.BindAsync( new IPEndPoint( IPAddress.Any, this.options.Port ) );

        this.logger.LogInformation( $"TcpService started - listening on {this.options.Port}" );
    }

    public async Task StopAsync( CancellationToken cancellation_token ) {
        // gracefully disconnect all clients
        return;
    }
}
