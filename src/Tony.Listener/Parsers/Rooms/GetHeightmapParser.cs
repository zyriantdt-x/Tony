using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 60 )]
internal class GetHeightmapParser : IParser<GetHeightMapMessage> {
    public GetHeightMapMessage Parse( Message message ) => new();
}
