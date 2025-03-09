using Tony.Listener.Composers.Room;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Events.Rooms;

namespace Tony.Listener.Subscribers.Handlers.Rooms;
[Queue("ROOM_ENTITIES_UPDATED")]
class RoomEntitiesUpdatedHandler : IHandler<RoomEntitiesUpdatedEvent> {
    private readonly TonyClientService clients;

    public RoomEntitiesUpdatedHandler( TonyClientService clients ) {
        this.clients = clients;
    }

    public async Task Handle( RoomEntitiesUpdatedEvent message ) {
        await this.clients.SendToMany( message.Audience, new UserObjectsComposer() {
            Entities = message.Entities
        } );
    }
}
