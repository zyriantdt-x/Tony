using Tony.Listener.Messages.Naivgator;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Navigator;
[Header( 150 )]
internal class NavigateParser : IParser<NavigateMessage> {
    public NavigateMessage Parse( Message message ) => new();
}
