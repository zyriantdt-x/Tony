using Tony.Revisions.V14.Messages.Rooms;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 59 )]
public class GoToRoomParser : IParser<GoToRoomMessage> {
    public GoToRoomMessage Parse( Message message ) {
        int room_id;

        try {
            room_id = Convert.ToInt32( System.Text.Encoding.Default.GetString( message.RemainingBytes.ToArray() ) );
        } catch( FormatException ) {
            return new();
        }

        return new() {
            RoomId = room_id
        };
    }
}
