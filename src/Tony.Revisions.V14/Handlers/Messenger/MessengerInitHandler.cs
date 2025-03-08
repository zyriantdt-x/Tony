using Tony.Listener.Composers.Messenger;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Messenger;
[Header( 12 )]
internal class MessengerInitHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new MessengerInitComposer() );
    }
}
