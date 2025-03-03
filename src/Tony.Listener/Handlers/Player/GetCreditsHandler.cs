using Tony.Listener.Messages.Player;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Player;
[Header( 8 )]
internal class GetCreditsHandler : IHandler<GetCreditsMessage> {
    public Task Handle( TonyClient client, GetCreditsMessage message ) {
    
    }
}
