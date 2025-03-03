using Tony.Listener.Messages.Handshake;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Handshake;
[Header(202)]
internal class GenerateKeyParser : IParser<GenerateKeyMessage> {
    public GenerateKeyMessage Parse( Message message ) => new();
}
