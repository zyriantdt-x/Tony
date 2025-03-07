using Tony.Listener.Messages.Messenger;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Messenger;
internal class MessengerInitParser : IParser<MessengerInitMessage> {
    public MessengerInitMessage Parse( Message message ) => new();
}
