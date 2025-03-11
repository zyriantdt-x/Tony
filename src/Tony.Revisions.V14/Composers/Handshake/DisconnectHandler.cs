using Tony.Revisions.V14.PubSub.Events.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Services;

namespace Tony.Revisions.V14.Composers.Handshake;
[Header( 137 )]
internal class DisconnectHandler : IHandler {
    private readonly IPlayerService player_service;
    private readonly IRoomEntityService entity_service;
    private readonly IPublisherService publisher;

    public DisconnectHandler( IPlayerService player_service, IRoomEntityService entity_service, IPublisherService publisher ) {
        this.player_service = player_service;
        this.entity_service = entity_service;
        this.publisher = publisher;
    }

    public async Task Handle( ITonyClient client, object message ) {
        PlayerRoomDto? room_data = await this.player_service.GetPlayerRoom( client.PlayerId );
        if( room_data is not null ) {
            await this.entity_service.RemoveEntityFromRoom( room_data.RoomId, room_data.InstanceId );

            IEnumerable<RoomEntityDto> entities = await this.entity_service.GetEntitiesInRoom( room_data.RoomId );
            await this.publisher.Publish( new RoomEntitiesUpdatedEvent() { // ffs i really ought to do something about this
                Audience = entities.Where( e => e.EntityType == EntityType.PLAYER ).Select( e => e.EntityId ),
                Entities = entities
            } );
        }
        
        // plan for other things here...
    }
}
