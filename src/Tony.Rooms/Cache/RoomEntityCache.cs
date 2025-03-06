using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StackExchange.Redis;
using System.Text.Json;
using Tony.Shared.Dto;

namespace Tony.Rooms.Cache;

public class RoomEntityCache {
    private readonly IDatabase redis;

    public RoomEntityCache( IConnectionMultiplexer redis ) {
        this.redis = redis.GetDatabase();
    }

    public async Task<ICollection<RoomEntityDto>> GetEntitiesInRoom( int room_id ) {
        string key = $"rooms:{room_id}:entities";
        HashEntry[] entities = await this.redis.HashGetAllAsync( key );

        return entities.Select( entity => JsonSerializer.Deserialize<RoomEntityDto>( entity.Value! ) )
                       .Where( dto => dto is not null )
                       .ToList()!;
    }

    public async Task AddEntityToRoom( int room_id, RoomEntityDto entity ) {
        string key = $"rooms:{room_id}:entities";
        string entity_serialised = JsonSerializer.Serialize( entity );

        await this.redis.HashSetAsync( key, entity.InstanceId, entity_serialised );
    }

    public async Task RemoveEntityFromRoom( int room_id, int instance_id ) {
        string key = $"rooms:{room_id}:entities";

        await this.redis.HashDeleteAsync( key, instance_id );
    }
}