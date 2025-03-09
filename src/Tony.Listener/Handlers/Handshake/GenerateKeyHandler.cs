using Tony.Listener.Composers.Handshake;
using Tony.Listener.Messages.Handshake;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Handshake;
[Header( 202 )]
internal class GenerateKeyHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new SessionParametersComposer() );
        await client.SendAsync( new AvailableSetsComposer() );
    }
}
