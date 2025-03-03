using Tony.Listener.Composers.Handshake;
using Tony.Listener.Messages.Handshake;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Handshake;
[Header(206)]
internal class InitCryptoHandler : IHandler<InitCryptoMessage> {
    public async Task Handle( TonyClient client, InitCryptoMessage message ) {
        await client.SendAsync( new CryptoParametersComposer() );
    }
}
