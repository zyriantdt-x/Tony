using Tony.Revisions.Composers.Room;
using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 57 )]
internal class TryRoomHandler : IHandler<TryRoomMessage> {
    public async Task Handle( TonyClient client, TryRoomMessage message ) {
        // todo : doorbell and password

        await client.SendAsync( new RoomLetInComposer() );
    }
}
