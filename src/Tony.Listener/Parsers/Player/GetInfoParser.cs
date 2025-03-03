using Tony.Listener.Messages.Player;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Player;
[Header( 7 )]
internal class GetInfoParser : IParser<GetInfoMessage> {
    public GetInfoMessage Parse( Message message ) => new();
}
