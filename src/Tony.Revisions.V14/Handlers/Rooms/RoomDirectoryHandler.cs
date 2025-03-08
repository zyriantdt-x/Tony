using Tony.Revisions.Composers.Room;
using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 2 )]
internal class RoomDirectoryHandler : IHandler<RoomDirectoryMessage> {
    public async Task Handle( TonyClient client, RoomDirectoryMessage message ) {
        if( !message.IsPublic ) {
            await client.SendAsync( new OpenConnectionComposer() );
            return;
        }

        // handle public room
    }
}
