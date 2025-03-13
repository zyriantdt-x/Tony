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

    public async Task<RoomEntityDto?> GetPlayerEntity( PlayerRoomDto player_room ) {
        string key = $"room:{player_room.RoomId}:entities";

        string? serialised_entity = await this.redis.HashGetAsync( key, player_room.InstanceId );
        if( serialised_entity is null )
            return null;

        return JsonSerializer.Deserialize<RoomEntityDto>( serialised_entity );
    }

    public async Task AddEntityToRoom( RoomEntityDto entity ) {
        string key = $"room:{entity.RoomId}:entities";
        string entity_serialised = JsonSerializer.Serialize( entity );

        await this.redis.HashSetAsync( key, entity.InstanceId, entity_serialised );
    }

    public async Task RemoveEntityFromRoom( RoomEntityDto entity ) {
        string key = $"room:{entity.RoomId}:entities";

        await this.redis.HashDeleteAsync( key, entity.InstanceId );
    }
}
