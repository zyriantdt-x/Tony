using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Tcp;

namespace Tony.Revisions.Parsers.Rooms;
[Header( 59 )]
internal class GoToRoomParser : IParser<GoToRoomMessage> {
    public GoToRoomMessage Parse( Message message ) {
        int room_id;

        try {
            room_id = Convert.ToInt32( System.Text.Encoding.Default.GetString( message.RemainingBytes.ToArray() ) );
        } catch (FormatException) {
            return new();
        }

        return new() {
            RoomId = room_id
        };
    }
}
