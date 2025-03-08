using Tony.Revisions.Composers.Messenger;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Messenger;
[Header( 12 )]
internal class MessengerInitHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new MessengerInitComposer() );
    }
}
