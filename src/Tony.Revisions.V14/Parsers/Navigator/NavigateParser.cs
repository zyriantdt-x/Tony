using Tony.Revisions.V14.Messages.Naivgator;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Navigator;
[Header( 150 )]
public class NavigateParser : IParser<NavigateClientMessage> {
    public NavigateClientMessage Parse( ClientMessage ClientMessage ) => new() {
        HideFull = ClientMessage.ReadInt() == 1,
        CategoryId = ClientMessage.ReadInt()
    };
}
