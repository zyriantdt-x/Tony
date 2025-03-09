using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Tony.Sdk.Clients;

namespace Tony.Server.Tcp.Codec;
internal class MessageEncoder : MessageToMessageEncoder<ClientMessage> {
    protected override void Encode( IChannelHandlerContext context, ClientMessage message, List<object> output ) {
        IByteBuffer buf = message.Buffer;
        buf.WriteByte( 1 );
        output.Add( buf );
    }
}
