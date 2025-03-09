using Tony.Shared.Dto;
using Tony.Rooms.Storage;
using Tony.Rooms.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Tony.Rooms.Cache;

namespace Tony.Rooms.Services;

public class NavigatorService {
    private readonly RoomStorage storage;
    private readonly NavigatorCache navi_cache;

    public NavigatorService( RoomStorage storage, NavigatorCache navi_cache ) {
        this.storage = storage;
        this.navi_cache = navi_cache;
    }

    public async Task<CategoryDto?> GetCategory( int id ) {
        CategoryDto? category = await this.navi_cache.GetCategory( id );

        // not in redis
        if(category is null) {
            Category? category_entity = await this.storage.Categories.FindAsync( id );
            if( category_entity is null )
                return null;

            category = this.MapCategoryDto( category_entity );
            await this.navi_cache.SaveCategory( category );
        }

        return category;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByParentId( int id ) {
        IEnumerable<CategoryDto>? subcategories = await this.navi_cache.GetSubcategories( id );

        // not in redis
        if( subcategories is null ) {
            List<Category> subcategory_entities = await this.storage.Categories.Where( c => c.ParentId == id ).ToListAsync();
            if( subcategory_entities.Count < 1 )
                return [];

            subcategories = subcategory_entities.Select( this.MapCategoryDto );
            await this.navi_cache.SaveSubcategories( id, subcategories );
        }

        return subcategories;
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {
        IEnumerable<NavNodeDto>? nav_nodes = await this.navi_cache.GetNavNodes( id );
        // not in redis
        if( nav_nodes is null ) {
            List<RoomData> room_entities = await this.storage.RoomData.Where( r => r.Category == id ).ToListAsync();
            if( room_entities.Count < 1 )
                return [];

            nav_nodes = room_entities.Select( room => new NavNodeDto() {
                Id = room.Id,
                IsPublicRoom = false,
                Name = room.Name,
                Description = room.Description,
                VisitorsMax = room.VisitorsMax,
                VisitorsNow = room.VisitorsNow,
                CategoryId = room.Category,
                Ccts = room.Ccts,
                OwnerId = room.OwnerId,
                AccessType = AccessType.OPEN
            } );
            await this.navi_cache.SaveNavNodes( id, nav_nodes );
        }

        return nav_nodes;
    }

    private CategoryDto MapCategoryDto( Category category )
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
