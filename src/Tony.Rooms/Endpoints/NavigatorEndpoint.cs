using Grpc.Core;
using Tony.Shared.Dto;
using Tony.Rooms.Services;
using Tony.Shared.Protos;
using Tony.Shared.Mappers;

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

        return category.ToProtobuf();
    }

    public async override Task<GetNavigatorCategoriesByParentIdResponse> GetNavigatorCategoriesByParentId( GetNavigatorCategoriesByParentIdRequest request, ServerCallContext context ) {
        IEnumerable<CategoryDto> categories = await this.navigator.GetCategoriesByParentId( request.ParentId );

        GetNavigatorCategoriesByParentIdResponse res = new();
        res.Categories.AddRange( categories.Select( category => category.ToProtobuf() ) );

        return res;
    }

    public async override Task<GetNavNodesByCategoryIdResponse> GetNavNodesByCategoryId( GetNavNodesByCategoryIdRequest request, ServerCallContext context ) {
        IEnumerable<NavNodeDto> nav_nodes = await this.navigator.GetNavNodesByCategoryId( request.Id );

        GetNavNodesByCategoryIdResponse res = new();
        res.Rooms.AddRange( nav_nodes.Select( node => node.ToProtobuf() ) );

        return res;
    }
}
