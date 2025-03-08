using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
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
