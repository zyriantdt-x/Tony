using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Shared.Mappers;
public static class CategoryMapper {
    public static CategoryDto ToDto( this GetCategoryByIdResponse category ) => new() {
        Id = category.Id,
        ParentId = category.ParentId,
        Name = category.Name,
        IsNode = category.IsNode,
        IsPublicSpace = category.IsPublicSpace,
        IsTradingAllowed = category.IsTradingAllowed,
        MinAccess = category.MinAccess,
        MinAssign = category.MinAssign
    };

    public static GetCategoryByIdResponse ToProtobuf( this CategoryDto category ) => new() {
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
