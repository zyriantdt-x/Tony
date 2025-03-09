using Tony.Revisions.V14.Composers.Room;
using Tony.Revisions.V14.PubSub.Events.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Handlers.Rooms;
[Event( "room_entities_updated" )]
class RoomEntitiesUpdatedHandler : IPubSubHandler<RoomEntitiesUpdatedEvent> {
    private readonly ITonyClientService clients;

    public RoomEntitiesUpdatedHandler( ITonyClientService clients ) {
        this.clients = clients;
    }

    public async Task Handle( RoomEntitiesUpdatedEvent message ) {
        await this.clients.SendToMany( message.Audience, new UserObjectsComposer() {
            Entities = message.Entities
        } );
    }
}
