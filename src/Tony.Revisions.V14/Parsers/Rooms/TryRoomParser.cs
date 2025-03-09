using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 57 )]
public class TryRoomParser : IParser<TryRoomMessage> {
    public TryRoomMessage Parse( ClientMessage message ) {
        string contents = System.Text.Encoding.Default.GetString( message.RemainingBytes.ToArray() );

        TryRoomMessage msg = new();

        if( contents.Length < 1 )
            return msg;

        if( contents.Contains( "/" ) ) {
            string[] split_contents = contents.Split( '/' );

            try { // this is gross
                msg.RoomId = Convert.ToInt32( split_contents[ 0 ] );
            } catch( FormatException ) {
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
