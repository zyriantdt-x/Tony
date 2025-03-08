using Tony.Revisions.Messages.Naivgator;
using Tony.Revisions.Tcp;

namespace Tony.Revisions.Parsers.Navigator;
[Header( 150 )]
internal class NavigateParser : IParser<NavigateMessage> {
    public NavigateMessage Parse( Message message ) => new() {
        HideFull = message.ReadInt() == 1,
        CategoryId = message.ReadInt()
    };
}
