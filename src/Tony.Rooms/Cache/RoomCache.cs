using StackExchange.Redis;
using System.Text.Json;
using Tony.Shared.Dto;

namespace Tony.Rooms.Cache;

public class RoomCache {
    private readonly IDatabase redis;

    public RoomCache( IConnectionMultiplexer redis ) {
        this.redis = redis.GetDatabase();
    }

    public async Task<RoomDataDto?> GetRoomDataById( int id ) {
        string room_key = $"room:{id}";

        string? serialised_room = await this.redis.StringGetAsync( room_key );
        if( serialised_room is null )
            return null;

        return JsonSerializer.Deserialize<RoomDataDto>( serialised_room );
    }

    public async Task SaveRoomData( RoomDataDto room ) {
        string room_key = $"room:{room.Id}";

        string? serialized_room = JsonSerializer.Serialize( room );
        if( serialized_room is null ) 
            return;

        await this.redis.StringSetAsync( room_key, serialized_room );
    }
}
