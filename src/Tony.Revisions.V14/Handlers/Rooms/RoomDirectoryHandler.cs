using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
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
