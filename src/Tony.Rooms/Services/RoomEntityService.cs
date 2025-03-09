using Tony.Rooms.Cache;
using Tony.Shared;
using Tony.Shared.Dto;
using Tony.Shared.Events.Rooms;

namespace Tony.Rooms.Services;

public class RoomEntityService {
    private readonly RoomEntityCache cache;
    private readonly PublisherService pub;

    public RoomEntityService( RoomEntityCache cache, PublisherService pub ) {
        this.cache = cache;
        this.pub = pub;
    }

    public Task<ICollection<RoomEntityDto>> GetEntitiesInRoom( int room_id ) => this.cache.GetEntitiesInRoom( room_id );

    public async Task AddEntityToRoom( int room_id, RoomEntityDto entity ) {
        await this.cache.AddEntityToRoom( room_id, entity );

        ICollection<RoomEntityDto> updated_entities = await this.GetEntitiesInRoom( room_id );
        await this.pub.Publish( new RoomEntitiesUpdatedEvent() {
            Audience = updated_entities.Where( e => e.EntityType == EntityType.PLAYER ).Where( e => e.InstanceId != entity.InstanceId ).Select( entity => entity.EntityId ).ToList(), // this is probably slow as fuck, if not it's still ugly
            Entities = updated_entities
        } );
    }

    public async Task RemoveEntityFromRoom( int room_id, int instance_id ) {
        await this.cache.RemoveEntityFromRoom( room_id, instance_id );

        ICollection<RoomEntityDto> updated_entities = await this.GetEntitiesInRoom( room_id );
        await this.pub.Publish( new RoomEntitiesUpdatedEvent() {
            Audience = updated_entities.Where( e => e.EntityType == EntityType.PLAYER ).Select( entity => entity.EntityId ).ToList(), // this is probably slow as fuck, if not it's still ugly
            Entities = updated_entities
        } );
    }

    public async Task<int?> GetPlayerInstanceId( int player_id ) {
        
    }
}
