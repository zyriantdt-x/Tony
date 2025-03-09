using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 182 )]
public class GetInterestHandler : IHandler {
    public async Task Handle( ITonyClient client, object message ) => await client.SendAsync( new RoomInterestComposer() );
}
