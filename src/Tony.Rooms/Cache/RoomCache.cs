using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public async Task<RoomDataDto?> GetPlayerRoomData( int player_id ) {
        string playermap_key = $"room:playermap:{player_id}";
        string? room_id = await this.redis.StringGetAsync( playermap_key );
        if( room_id is null )
            return null;

        return await this.GetRoomDataById( Convert.ToInt32( room_id ) );
    }

    public async Task<RoomModelDto?> GetRoomModelById( string model_id ) {
        string model_key = $"room:models:{model_id}";
        string? model = await this.redis.StringGetAsync( model_key );
        if( model is null )
            return null;

        return JsonSerializer.Deserialize<RoomModelDto>( model );
    }

    public async Task SaveRoomModel( RoomModelDto model ) {
        string model_key = $"room:models:{model.Id}";
        string? serialised_model = JsonSerializer.Serialize( model );
        if( serialised_model is null )
            return;

        await this.redis.StringSetAsync( model_key, serialised_model );
    }
}
