using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Tony.Sdk.Clients;
using Tony.Sdk.Encoding;

namespace Tony.Server.Tcp.Codec;
internal class MessageDecoder : ByteToMessageDecoder {
    protected override void Decode( IChannelHandlerContext context, IByteBuffer input, List<object> output ) {
        if( input.ReadableBytes < 5 )
            return;

        input.MarkReaderIndex();
        int length = Base64Encoding.Decode( [ input.ReadByte(), input.ReadByte(), input.ReadByte() ] );

        if( input.ReadableBytes < length ) {
            input.ResetReaderIndex();
            return;
        }

        if( length < 0 )
            return;

        ClientMessage message = new( input.ReadBytes( length ) );

        output.Add( message );
    }
}
