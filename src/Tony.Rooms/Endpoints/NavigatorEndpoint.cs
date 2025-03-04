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

        return new() {
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
}
