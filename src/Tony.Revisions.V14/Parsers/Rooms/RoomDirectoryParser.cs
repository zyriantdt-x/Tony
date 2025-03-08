using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 2 )]
public class RoomDirectoryParser : IParser<RoomDirectoryClientMessage> {
    public RoomDirectoryClientMessage Parse( ClientMessage ClientMessage )
        => new() {
            IsPublic = System.Text.Encoding.Default.GetString( ClientMessage.RemainingBytes.ToArray() )[ 0 ] == 'A'
        };
}
