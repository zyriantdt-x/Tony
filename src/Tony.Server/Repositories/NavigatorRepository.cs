using Microsoft.EntityFrameworkCore;
using Tony.Sdk.Dto;
using Tony.Server.Storage;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Repositories;
internal class NavigatorRepository {
    private readonly TonyStorage storage;

    public NavigatorRepository( IDbContextFactory<TonyStorage> storage_factory ) {
        this.storage = storage_factory.CreateDbContext();
    }

    public async Task<NavigatorCategoryDto?> GetCategoryById( int id ) {
        NavigatorCategory? category = await this.storage.NavigatorCategories.FindAsync( id );
        if( category is null )
            return null;

        return this.MapEntityToDto( category );
    }

    public async Task<IEnumerable<NavigatorCategoryDto>> GetCategoriesByParentId( int id ) {
        List<NavigatorCategory> subcategory_entities = await this.storage.NavigatorCategories.Where( c => c.ParentId == id ).ToListAsync();
        if( subcategory_entities.Count < 1 )
            return [];

        IEnumerable<NavigatorCategoryDto> subcategories = subcategory_entities.Select( this.MapEntityToDto );

        return subcategories;
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {

    }

    private NavigatorCategoryDto MapEntityToDto( NavigatorCategory category )
        => new() {
            Id = category.Id,
            ParentId = category.ParentId,
            Name = category.Name,
            IsNode = category.IsNode,
            IsPublicSpace = category.IsPublicSpace,
            IsTradingAllowed = category.IsTradingAllowed,
            MinAccess = category.MinAccess,
            MinAssign = category.MinAssign
        };
}
