using StackExchange.Redis;
using System.Text.Json;
using Tony.Rooms.Cache;
using Tony.Shared.Dto;

namespace Tony.Rooms.Services;

public class RoomEntityService {
    private readonly RoomEntityCache cache;
    
    public RoomEntityService( RoomEntityCache cache ) {
        this.cache = cache;
    }

    public Task<ICollection<RoomEntityDto>> GetEntitiesInRoom( int room_id ) => this.cache.GetEntitiesInRoom( room_id );

    public Task AddEntityToRoom( int room_id, RoomEntityDto entity ) => this.cache.AddEntityToRoom( room_id, entity );

    public Task RemoveEntityFromRoom( int room_id, int instance_id ) => this.cache.RemoveEntityFromRoom( room_id, instance_id );
}
