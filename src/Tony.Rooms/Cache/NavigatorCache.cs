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

    public async Task<IEnumerable<CategoryDto>?> GetSubcategories( int parent_id ) {
        string subcategories_key = $"navigator:{parent_id}:subcategories";

        string? serialised_subcategories = await this.redis.StringGetAsync( subcategories_key );
        if( serialised_subcategories is null )
            return null;

        return JsonSerializer.Deserialize<IEnumerable<CategoryDto>>( serialised_subcategories );
    }

    public async Task SaveCategory( CategoryDto category ) {
        string category_key = $"navigator:{category.Id}";

        string serialised_category = JsonSerializer.Serialize( category );

        await this.redis.StringSetAsync( category_key, serialised_category );
    }

    public async Task SaveSubcategories( int parent_id, IEnumerable<CategoryDto> children ) {
        string subcategory_key = $"navigator:{parent_id}:subcategories";

        string serialised_subcategories = JsonSerializer.Serialize( children );

        await this.redis.StringSetAsync( subcategory_key, serialised_subcategories );
    }

    public async Task RemoveCategory( int id ) {
        string category_key = $"navigator:{id}";

        await this.redis.KeyDeleteAsync( category_key );
    }

    public Task RemoveCategory( CategoryDto category )
        => this.RemoveCategory( category.Id );

    public async Task<IEnumerable<NavNodeDto>?> GetNavNodes( int category_id ) {
        string nodes_key = $"navigator:{category_id}:nodes";

        string? serialised_nodes = await this.redis.StringGetAsync( nodes_key );
        if( serialised_nodes is null )
            return null;

        return JsonSerializer.Deserialize<IEnumerable<NavNodeDto>>( serialised_nodes );
    }

    public async Task SaveNavNodes( int category_id, IEnumerable<NavNodeDto> nodes ) {
        string nodes_key = $"navigator:{category_id}:nodes";

        string serialised_nodes = JsonSerializer.Serialize( nodes );

        await this.redis.StringSetAsync( nodes_key, serialised_nodes );
    }
}
