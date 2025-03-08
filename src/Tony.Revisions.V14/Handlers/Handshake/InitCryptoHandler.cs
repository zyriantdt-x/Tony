using Tony.Revisions.Composers.Handshake;
using Tony.Revisions.Messages.Handshake;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Handshake;
[Header(206)]
internal class InitCryptoHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) {
        await client.SendAsync( new CryptoParametersComposer() );
    }
}
