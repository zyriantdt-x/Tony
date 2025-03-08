using Tony.Revisions.V14.Composers.Room;
using Tony.Revisions.V14.Messages.Rooms;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 57 )]
public class TryRoomHandler : IHandler<TryRoomMessage> {
    public async Task Handle( TonyClient client, TryRoomMessage message ) =>
        // todo : doorbell and password

        await client.SendAsync( new RoomLetInComposer() );
}
