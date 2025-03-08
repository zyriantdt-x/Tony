using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 2 )]
internal class RoomDirectoryParser : IParser<RoomDirectoryMessage> {
    public RoomDirectoryMessage Parse( Message message )
        => new() {
            IsPublic = System.Text.Encoding.Default.GetString( message.RemainingBytes.ToArray() )[ 0 ] == 'A'
        };
}
