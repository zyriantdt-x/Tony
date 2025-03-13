using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Clients;
using Tony.Sdk.PubSub;
using Tony.Sdk.PubSub.Events.Player;

namespace Tony.Revisions.V14.PubSub.Handlers.Rooms;
[Event( "room_chat" )]
internal class RoomChatHandler : IPubSubHandler<RoomChatEvent> {
    private readonly ITonyClientService clients;

    public RoomChatHandler( ITonyClientService clients ) {
        this.clients = clients;
    }

    public async Task Handle( RoomChatEvent message ) {
        await this.clients.SendToMany( message.Audience, new ChatMessageComposer() {
            InstanceId = message.SenderInstanceId,
            Message = message.Message
        } );
    }
}
