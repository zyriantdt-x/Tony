using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 57 )]
public class TryRoomHandler : IHandler<TryRoomClientMessage> {
    public async Task Handle( ITonyClient client, TryRoomClientMessage ClientMessage ) =>
        // todo : doorbell and password

        await client.SendAsync( new RoomLetInComposer() );
}
