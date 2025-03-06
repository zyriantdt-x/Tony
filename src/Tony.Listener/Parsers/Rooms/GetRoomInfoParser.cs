using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
internal class GetRoomInfoParser : IParser<GetRoomInfoMessage> {
    public GetRoomInfoMessage Parse( Message message )
        => new() { RoomId = Convert.ToInt32( System.Text.Encoding.Default.GetString( message.Body.ToArray() ) ) };
}
