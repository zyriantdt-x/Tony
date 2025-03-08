using Tony.Listener.Composers.Room;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 63 )]
internal class GetItemsHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new ItemsComposer() );
    }
}
