using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Tony.Listener.Tcp.Codec;
internal class MessageEncoder : MessageToMessageEncoder<Message> {
    protected override void Encode( IChannelHandlerContext context, Message message, List<object> output ) {
        IByteBuffer buf = message.Buffer;
        buf.WriteByte( 1 );
        output.Add( buf );
    }
}
