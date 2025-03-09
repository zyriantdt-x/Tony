using StackExchange.Redis;
using System.Text.Json;
using Tony.Sdk.Dto;

namespace Tony.Server.Cache;
internal class PlayerCache {
    private readonly IDatabase redis;

    public PlayerCache( IConnectionMultiplexer redis ) {
        this.redis = redis.GetDatabase();
    }

    public async Task<PlayerDto?> GetPlayer( int id ) {
        string player_key = $"player:{id}";

        string? serialised_player = await this.redis.StringGetAsync( player_key );

        if( serialised_player is null )
            return null;

        return JsonSerializer.Deserialize<PlayerDto>( serialised_player );
    }

    public async Task SavePlayer( PlayerDto player ) {
        string player_key = $"player:{player.Id}";

        string serialised_player = JsonSerializer.Serialize( player );

        await this.redis.StringSetAsync( player_key, serialised_player );
    }

    public async Task RemovePlayer( int id ) {
        string player_key = $"player:{id}";

        await this.redis.KeyDeleteAsync( player_key );
    }

    public Task RemovePlayer( PlayerDto player )
        => this.RemovePlayer( player.Id );

    public async Task<PlayerRoomDto?> GetPlayerRoom( int id ) {
        string player_map_key = $"player:{id}:room";

        string? serialised_dto = await this.redis.StringGetAsync( player_map_key );
        if( serialised_dto is null )
            return null;

        return JsonSerializer.Deserialize<PlayerRoomDto>( serialised_dto );
    }

    public async Task SetPlayerRoom( int id, PlayerRoomDto? dto = null ) {
        string player_map_key = $"player:{id}:room";

        if( dto is null ) {
            await this.redis.KeyDeleteAsync( player_map_key );
            return;
        }

        string serialised_dto = JsonSerializer.Serialize( dto );

        await this.redis.StringSetAsync( player_map_key, serialised_dto );
    }
}
