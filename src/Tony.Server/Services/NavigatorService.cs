using Tony.Sdk.Dto;
using Tony.Sdk.Services;
using Tony.Server.Cache;
using Tony.Server.Repositories;

namespace Tony.Server.Services;
internal class NavigatorService : INavigatorService {
    private readonly NavigatorCache cache;
    private readonly NavigatorRepository repository;

    public NavigatorService( NavigatorRepository repository, NavigatorCache cache ) {
        this.cache = cache;
        this.repository = repository;
    }

    public async Task<IEnumerable<NavigatorCategoryDto>> GetCategoriesByParentId( int id ) {
        IEnumerable<NavigatorCategoryDto> categories = await this.cache.GetSubcategories( id ) ?? await this.repository.GetCategoriesByParentId( id );
        if( categories.Any() )
            await this.cache.SaveSubcategories( id, categories );

        return categories;
    }

    public async Task<NavigatorCategoryDto?> GetCategory( int id ) {
        NavigatorCategoryDto? category = await this.cache.GetCategory( id ) ?? await this.repository.GetCategoryById( id );
        if( category is not null )
            await this.cache.SaveCategory( category );

        return category;
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {
        IEnumerable<NavNodeDto> nodes = await this.cache.GetNavNodes( id ) ?? await this.repository.GetNavNodesByCategoryId( id );
        if( nodes.Any() )
            await this.cache.SaveNavNodes( id, nodes );

        return nodes;
    }
}
