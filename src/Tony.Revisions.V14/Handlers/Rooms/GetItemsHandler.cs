using Tony.Revisions.V14.Composers.Room;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 63 )]
public class GetItemsHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) => await client.SendAsync( new ItemsComposer() );
}
