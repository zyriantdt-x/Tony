using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 21 )]
public class GetRoomInfoParser : IParser<GetRoomInfoMessage> {
    public GetRoomInfoMessage Parse( ClientMessage ClientMessage )
        => new() { RoomId = Convert.ToInt32( System.Text.Encoding.Default.GetString( ClientMessage.RemainingBytes ) ) };
}
