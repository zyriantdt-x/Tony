using Tony.Listener.Messages.Naivgator;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Navigator;
[Header( 150 )]
internal class NavigateHandler : IHandler<NavigateMessage> {
    public async Task Handle( TonyClient client, NavigateMessage message ) {
    
    }
}
