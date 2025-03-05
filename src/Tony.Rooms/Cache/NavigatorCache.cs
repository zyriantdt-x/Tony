using StackExchange.Redis;
using System.Text.Json;
using Tony.Rooms.Storage.Entities;
using Tony.Shared.Dto;

namespace Tony.Rooms.Cache;

public class NavigatorCache {
    private readonly IDatabase redis;

    public NavigatorCache( IConnectionMultiplexer redis ) {
        this.redis = redis.GetDatabase();
    }

    public async Task<CategoryDto?> GetCategory( int id ) {
        string category_key = $"navigator:{id}";

        string? serialised_category = await this.redis.StringGetAsync( category_key );
        if( serialised_category is null )
            return null;

        return JsonSerializer.Deserialize<CategoryDto>( serialised_category );
    }

    public async Task SaveCategory( CategoryDto category ) {
        string category_key = $"navigator:{category.Id}";

        string serialised_category = JsonSerializer.Serialize(category );

        await this.redis.StringSetAsync( category_key, serialised_category );
    }

    public async Task RemoveCategory( int id ) {
        string category_key = $"navigator:{id}";

        await this.redis.KeyDeleteAsync( category_key );
    }

    public Task RemoveCategory( CategoryDto category )
        => this.RemoveCategory( category.Id );
}
