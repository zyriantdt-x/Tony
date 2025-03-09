using StackExchange.Redis;
using System.Text.Json;
using Tony.Sdk.Dto;

namespace Tony.Server.Cache;
internal class RoomEntityCache {
    private readonly IDatabase redis;

    public RoomEntityCache( IConnectionMultiplexer redis ) {
        this.redis = redis.GetDatabase(); ;
    }

    public async Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int room_id ) {
        string key = $"room:{room_id}:entities";
        HashEntry[] entities = await this.redis.HashGetAllAsync( key );

        return entities.Select( entity => JsonSerializer.Deserialize<RoomEntityDto>( entity.Value! )! );
    }

    public async Task AddEntityToRoom( int room_id, RoomEntityDto entity ) {
        string key = $"room:{room_id}:entities";
        string entity_serialised = JsonSerializer.Serialize( entity );

        await this.redis.HashSetAsync( key, entity.InstanceId, entity_serialised );
    }

    public async Task RemoveEntityFromRoom( int room_id, int instance_id ) {
        string key = $"room:{room_id}:entities";

        await this.redis.HashDeleteAsync( key, instance_id );
    }
}
