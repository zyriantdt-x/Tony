using Tony.Revisions.V14.ClientMessages.Naivgator;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Navigator;
[Header( 150 )]
public class NavigateParser : IParser<NavigateClientMessage> {
    public NavigateClientMessage Parse( ClientMessage ClientMessage ) => new() {
        HideFull = ClientMessage.ReadInt() == 1,
        CategoryId = ClientMessage.ReadInt()
    };
}
