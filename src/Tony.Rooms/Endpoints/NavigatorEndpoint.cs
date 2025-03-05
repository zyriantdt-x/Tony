using Grpc.Core;
using Tony.Shared.Dto;
using Tony.Rooms.Services;
using Tony.Shared.Protos;

namespace Tony.Rooms.Endpoints;

public class NavigatorEndpoint : Shared.Protos.NavigatorEndpoint.NavigatorEndpointBase {
    private readonly NavigatorService navigator;

    public NavigatorEndpoint( NavigatorService navigator ) {
        this.navigator = navigator;
    }

    public async override Task<GetCategoryByIdResponse> GetCategoryById( GetCategoryByIdRequest request, ServerCallContext context ) {
        CategoryDto? category = await this.navigator.GetCategory( request.Id );
        if( category is null )
            return null;

        return this.MapCategoryResponse( category );
    }

    public async override Task<GetNavigatorCategoriesByParentIdResponse> GetNavigatorCategoriesByParentId( GetNavigatorCategoriesByParentIdRequest request, ServerCallContext context ) {
        IEnumerable<CategoryDto> categories = await this.navigator.GetCategoriesByParentId( request.ParentId );

        GetNavigatorCategoriesByParentIdResponse res = new();
        res.Categories.AddRange( categories.Select( this.MapCategoryResponse ) );

        return res;
    }

    public async override Task<GetNavNodesByCategoryIdResponse> GetNavNodesByCategoryId( GetNavNodesByCategoryIdRequest request, ServerCallContext context ) {
        IEnumerable<NavNodeDto> nav_nodes = await this.navigator.GetNavNodesByCategoryId( request.Id );

        GetNavNodesByCategoryIdResponse res = new();
        res.Rooms.AddRange( nav_nodes.Select( room => new NavNode() {
            Id = room.Id,
            IsPublicRoom = false,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            CategoryId = room.CategoryId,
            Ccts = room.Ccts,
            OwnerName = room.OwnerName,
            AccessType = (int)room.AccessType
        } ) );

        return res;
    }

    private GetCategoryByIdResponse MapCategoryResponse( CategoryDto category )
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
