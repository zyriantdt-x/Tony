using Tony.Revisions.V14.Composers.Room;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 182 )]
public class GetInterestHandler : IHandler {
    public async Task Handle( TonyClient client, object ClientMessage ) => await client.SendAsync( new RoomInterestComposer() );
}
