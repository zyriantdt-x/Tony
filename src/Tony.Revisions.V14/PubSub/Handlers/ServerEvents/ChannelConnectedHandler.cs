using Tony.Revisions.V14.Composers.Handshake;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Revisions.PubSub.ServerEvents;

namespace Tony.Revisions.V14.PubSub.Handlers.ServerEvents;
[Event( "channel_connected" )]
internal class ChannelConnectedHandler : IPubSubHandler<ChannelConnectedEvent> {
    private readonly ITonyClientService client_service;

    public ChannelConnectedHandler( ITonyClientService client_service ) {
        this.client_service = client_service;
    }

    // ive realised after the fact that this is a stupid idea - we're publishing this data when only 1 server actually needs to receive it lol
    public async Task Handle( ChannelConnectedEvent message ) {
        ITonyClient? client = this.client_service.GetClient( message.ClientId );
        if( client is null )
            return;

        await client.SendAsync( new HelloComposer() );
    }
}
