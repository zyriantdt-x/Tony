using Tony.Listener.Messages.Handshake;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Handshake;
[Header(206)]
internal class InitCryptoParser : IParser<InitCryptoMessage> {
    public InitCryptoMessage Parse( Message message ) => new();
}
