using Tony.Revisions.V14.PubSub.Events.Player;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Handlers.Player;
[Event( "login" )]
internal class LoginHandler : IPubSubHandler<LoginEvent> {
    private readonly ITonyClientService clients;

    public LoginHandler( ITonyClientService client ) {
        this.clients = client;
    }

    public async Task Handle( LoginEvent message ) {
        //await this.client.SendToAll( new AlertComposer() { Message = $"New user sign in: {message.Id} / {message.Username}" } );
        ITonyClient? client = this.clients.GetClient( message.PlayerId );
        if( client is null )
            return;

        if( client.Uuid != message.ConnectionId )
            await client.Kill();
    }
}
