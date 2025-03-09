using DotNetty.Transport.Channels;
using Tony.Server.Tcp.Codec;

namespace Tony.Server.Tcp;
internal class TonyChannelInitialiser : ChannelInitializer<IChannel> {
    private readonly ChannelHandlerAdapter handler;

    public TonyChannelInitialiser( ChannelHandlerAdapter handler ) {
        this.handler = handler;
    }

    protected override void InitChannel( IChannel channel ) {
        IChannelPipeline pipeline = channel.Pipeline;

        pipeline.AddLast( "gameEncoder", new MessageEncoder() );
        pipeline.AddLast( "gameDecoder", new MessageDecoder() );
        pipeline.AddLast( "clientHandler", this.handler );
    }
}
