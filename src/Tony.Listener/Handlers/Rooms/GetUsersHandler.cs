using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 63 )]
class GetUsersHandler : IHandler<GetUsersMessage> {
    public async Task Handle( TonyClient client, GetUsersMessage message ) {
    
    }
}
