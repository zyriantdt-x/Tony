using StackExchange.Redis;
using System.Text.Json;
using Tony.Sdk.Dto;

namespace Tony.Server.Cache;
internal class PlayerCache {
    private readonly IConnectionMultiplexer redis;

    public PlayerCache( IConnectionMultiplexer redis ) {
        this.redis = redis;
    }

    public async Task<PlayerDto?> GetPlayer( int id ) {
        IDatabase db = this.redis.GetDatabase();
        string player_key = $"player:{id}";

        string? serialised_player = await db.StringGetAsync( player_key );

        if( serialised_player is null )
            return null;

        return JsonSerializer.Deserialize<PlayerDto>( serialised_player );
    }

    public async Task SavePlayer( PlayerDto player ) {
        IDatabase db = this.redis.GetDatabase();
        string player_key = $"player:{player.Id}";

        string serialised_player = JsonSerializer.Serialize( player );

        await db.StringSetAsync( player_key, serialised_player );
    }

    public async Task RemovePlayer( int id ) {
        IDatabase db = this.redis.GetDatabase();
        string player_key = $"player:{id}";

        await db.KeyDeleteAsync( player_key );
    }

    public Task RemovePlayer( PlayerDto player )
        => this.RemovePlayer( player.Id );
}
