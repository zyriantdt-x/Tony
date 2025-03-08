using Tony.Revisions.Composers.Room;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 63 )]
internal class GetItemsHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new ItemsComposer() );
    }
}
