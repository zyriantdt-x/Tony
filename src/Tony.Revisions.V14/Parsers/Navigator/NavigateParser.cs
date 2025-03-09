using Tony.Revisions.V14.Messages.Naivgator;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Navigator;
[Header( 150 )]
public class NavigateParser : IParser<NavigateMessage> {
    public NavigateMessage Parse( ClientMessage message ) => new() {
        HideFull = message.ReadInt() == 1,
        CategoryId = message.ReadInt()
    };
}
