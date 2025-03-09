using Microsoft.EntityFrameworkCore;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
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

        return this.MapCategoryToDto( category );
    }

    public async Task<IEnumerable<NavigatorCategoryDto>> GetCategoriesByParentId( int id ) {
        List<NavigatorCategory> subcategory_entities = await this.storage.NavigatorCategories.Where( c => c.ParentId == id ).ToListAsync();
        if( subcategory_entities.Count < 1 )
            return [];

        return subcategory_entities.Select( this.MapCategoryToDto );
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {
        List<RoomData> rooms = await this.storage.RoomData.Include( r => r.Owner ).Where( r => r.Category == id ).ToListAsync();
        if( rooms.Count < 1 )
            return [];

        return rooms.Select( this.MapNavNodeToDto );
    }

    private NavigatorCategoryDto MapCategoryToDto( NavigatorCategory category )
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

    private NavNodeDto MapNavNodeToDto( RoomData room )
        => new() {
            Id = room.Id,
            IsPublicRoom = false,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            CategoryId = room.Category,
            Ccts = room.Ccts,
            AccessType = ( AccessType )room.AccessType,
            OwnerName = room.Owner.Username
        };
}
