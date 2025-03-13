using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Sdk.PubSub;
using Tony.Sdk.PubSub.Events.Player;
using Tony.Sdk.Services;
using Tony.Server.Cache;

namespace Tony.Server.Services;
internal class RoomEntityService : IRoomEntityService {
    private readonly RoomEntityCache cache;
    private readonly IPublisherService publisher;

    public RoomEntityService( RoomEntityCache cache, IPublisherService publisher ) {
        this.cache = cache;
        this.publisher = publisher;
    }

    public Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int room_id )
        => this.cache.GetEntitiesInRoom( room_id );

    public Task<RoomEntityDto?> GetPlayerEntity( PlayerRoomDto player_room )
        => this.cache.GetPlayerEntity( player_room );

    public async Task AddEntityToRoom( RoomEntityDto entity ) {
        await this.cache.AddEntityToRoom( entity );
        await this.SendUpdate( entity.RoomId );
    }

    public async Task RemoveEntityFromRoom( RoomEntityDto entity ) {
        await this.cache.RemoveEntityFromRoom( entity );
        await this.SendUpdate( entity.RoomId );
    }

    public async Task EntityChat( RoomEntityDto entity, ChatType chat_type, string message ) {
        // chat log etc

        IEnumerable<RoomEntityDto> audience = (await this.GetEntitiesInRoom( entity.RoomId )).Where( e => e.EntityType == EntityType.PLAYER );
        if( !audience.Any() )
            return;

        await this.publisher.Publish( new RoomChatEvent() {
            Audience = audience.Select( e => e.EntityId ),
            SenderInstanceId = entity.InstanceId,
            Type = chat_type,
            Message = message
        } );
    }

    private async Task SendUpdate( int room_id ) {
        IEnumerable<RoomEntityDto> updated_entities = await this.GetEntitiesInRoom( room_id );

        await this.publisher.Publish( new RoomEntitiesUpdatedEvent() {
            Audience = updated_entities.Where( entity => entity.EntityType == EntityType.PLAYER ).Select( entity => entity.EntityId ),
            Entities = updated_entities
        } );
    }
}
