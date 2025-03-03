using Tony.Listener.Messages.Player;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Player;
[Header( 8 )]
internal class GetCreditsParser : IParser<GetCreditsMessage> {
    public GetCreditsMessage Parse( Message message ) => new();
}
