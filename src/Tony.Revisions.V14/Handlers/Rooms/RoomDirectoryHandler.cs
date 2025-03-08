using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 2 )]
public class RoomDirectoryHandler : IHandler<RoomDirectoryClientMessage> {
    public async Task Handle( ITonyClient client, RoomDirectoryClientMessage ClientMessage ) {
        if( !ClientMessage.IsPublic ) {
            await client.SendAsync( new OpenConnectionComposer() );
            return;
        }

        // handle public room
    }
}
