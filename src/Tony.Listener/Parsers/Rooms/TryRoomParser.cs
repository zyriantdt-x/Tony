using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 57 )]
internal class TryRoomParser : IParser<TryRoomMessage> {
    public TryRoomMessage Parse( Message message ) {
        string contents = System.Text.Encoding.Default.GetString( message.RemainingBytes.ToArray() );

        TryRoomMessage msg = new();

        if( contents.Length < 1 )
            return msg;

        if( contents.Contains( "/" ) ) {
            string[] split_contents = contents.Split( '/' );

            try { // this is gross
                msg.RoomId = Convert.ToInt32( split_contents[ 0 ] );
            } catch(FormatException) {
                return msg;
            }

            msg.Password = split_contents[ 1 ];
        } else {
            try { // the fact we have to do it twice is worse
                msg.RoomId = Convert.ToInt32( contents );
            } catch( FormatException ) {
                return msg;
            }
        }

        return msg;
    }
}
