using Tony.Revisions.Composers.Room;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 62 )]
internal class GetObjectsHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendQueued( new ObjectsWorldComposer() );
        await client.SendQueued( new ActiveObjectsComposer() );

        client.Flush();
    }
}
