using Tony.Listener.Composers.Room;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 62 )]
internal class GetObjectsHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendQueued( new ObjectsWorldComposer() );
        await client.SendQueued( new ActiveObjectsComposer() );

        client.Flush();
    }
}
