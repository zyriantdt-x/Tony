using Tony.Sdk.Clients;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Revisions.PubSub.ServerEvents;

namespace Tony.Revisions.V14.PubSub.Handlers.ServerEvents;
[Event( "channel_disconnected" )]
internal class ChannelDisconnectedHandler : IPubSubHandler<ChannelDisconnectedEvent> {
    private readonly ITonyClientService client_service;

    public ChannelDisconnectedHandler( ITonyClientService client_service ) {
        this.client_service = client_service;
    }

    public async Task Handle( ChannelDisconnectedEvent message ) {
        ITonyClient? client = this.client_service.GetClient( message.ClientId );
        if( client is null )
            return;
    }
}
