using Tony.Shared.Dto;
using Tony.Rooms.Storage;
using Tony.Rooms.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tony.Rooms.Services;

public class NavigatorService {
    private readonly RoomStorage storage;

    public NavigatorService( RoomStorage storage ) {
        this.storage = storage;
    }

    public async Task<CategoryDto?> GetCategory( int id ) {
        Category? category_entity = await this.storage.Categories.FindAsync( id );
        return category_entity is null ? null : this.MapCategoryDto( category_entity );
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByParentId( int id ) {
        List<Category> category_entities = await this.storage.Categories.Where( c => c.ParentId == id ).ToListAsync();

        return category_entities.Select( c => this.MapCategoryDto( c ) );
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {
        List<RoomData> room_entities = await this.storage.RoomData.Where( r => r.Category == id ).ToListAsync();

        return room_entities.Select( room => new NavNodeDto() {
            Id = room.Id,
            IsPublicRoom = false,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            CategoryId = room.Category,
            Ccts = room.Ccts,
            OwnerName = "ellis",
            AccessType = AccessType.OPEN
        } );
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
