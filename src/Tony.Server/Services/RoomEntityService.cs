using Tony.Sdk.Dto;
using Tony.Sdk.Services;
using Tony.Server.Cache;

namespace Tony.Server.Services;
internal class RoomEntityService : IRoomEntityService {
    private readonly RoomEntityCache cache;

    public RoomEntityService( RoomEntityCache cache ) {
        this.cache = cache;
    }

    public Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int room_id )
        => this.cache.GetEntitiesInRoom( room_id );

    public Task AddEntityToRoom( int room_id, RoomEntityDto entity )
        => this.cache.AddEntityToRoom( room_id, entity );

    public Task RemoveEntityFromRoom( int room_id, int instance_id ) 
        => this.cache.RemoveEntityFromRoom( room_id, instance_id );
}
