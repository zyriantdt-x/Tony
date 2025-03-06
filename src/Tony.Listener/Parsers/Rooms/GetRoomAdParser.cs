using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 126 )]
internal class GetRoomAdParser : IParser<GetRoomAdMessage> {
    public GetRoomAdMessage Parse( Message message ) => new();
}