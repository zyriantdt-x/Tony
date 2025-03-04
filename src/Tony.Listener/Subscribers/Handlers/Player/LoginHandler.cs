using Tony.Listener.Composers.Alerts;
using Tony.Listener.Tcp.Clients;
using Tony.Protos;
using Tony.Protos.Player;

namespace Tony.Listener.Subscribers.Handlers.Player;
[Queue("login")]
internal class LoginHandler : IHandler<LoginEvent> {
    private readonly TonyClientService client;

    public LoginHandler( TonyClientService client ) {
        this.client = client;
    }

    public async Task Handle( LoginEvent message ) {
        await this.client.SendToAll( new AlertComposer() { Message = $"New user sign in: {message.Id} / {message.Username}" } );
    }
}
