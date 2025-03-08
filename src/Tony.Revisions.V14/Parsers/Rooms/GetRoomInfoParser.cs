using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Tcp;

namespace Tony.Revisions.Parsers.Rooms;
[Header( 21 )]
internal class GetRoomInfoParser : IParser<GetRoomInfoMessage> {
    public GetRoomInfoMessage Parse( Message message )
        => new() { RoomId = Convert.ToInt32( System.Text.Encoding.Default.GetString( message.RemainingBytes ) ) };
}
