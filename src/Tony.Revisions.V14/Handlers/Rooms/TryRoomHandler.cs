using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
using Tony.Sdk.Clients;
namespace Tony.Revisions.V14.Handlers.Rooms;

[Header( 57 )]
public class TryRoomHandler : IHandler<TryRoomMessage> {
    public async Task Handle( ITonyClient client, TryRoomMessage message ) =>
        // todo : doorbell and password

        await client.SendAsync( new RoomLetInComposer() );
}
