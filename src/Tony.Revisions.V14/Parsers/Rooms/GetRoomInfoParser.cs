using Tony.Revisions.V14.ClientMessages.Rooms;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 21 )]
public class GetRoomInfoParser : IParser<GetRoomInfoClientMessage> {
    public GetRoomInfoClientMessage Parse( ClientMessage ClientMessage )
        => new() { RoomId = Convert.ToInt32( System.Text.Encoding.Default.GetString( ClientMessage.RemainingBytes ) ) };
}
