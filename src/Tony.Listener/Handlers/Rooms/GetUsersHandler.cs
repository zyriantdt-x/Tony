using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
class GetUsersHandler : IHandler<GetUsersMessage> {
    public async Task Handle( TonyClient client, GetUsersMessage message ) {
    
    }
}
