using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 57 )]
internal class TryRoomHandler : IHandler<TryRoomMessage> {
    public async Task Handle( TonyClient client, TryRoomMessage message ) {
        // todo : doorbell and password

        await client.SendAsync( new RoomLetInComposer() );
    }
}
