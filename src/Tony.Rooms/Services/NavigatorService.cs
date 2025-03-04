using Tony.Shared.Dto;
using Tony.Rooms.Storage;
using Tony.Rooms.Storage.Entities;

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
