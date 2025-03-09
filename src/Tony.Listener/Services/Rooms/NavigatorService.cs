using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
using Tony.Shared.Protos;
using Tony.Shared.Mappers;

namespace Tony.Listener.Services.Rooms;
internal class NavigatorService {
    private readonly NavigatorEndpoint.NavigatorEndpointClient client;

    public NavigatorService( IOptions<ServiceOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.RoomServiceAddress ) );
    }

    public async Task<CategoryDto?> GetCategory( int id ) {
        GetCategoryByIdResponse? res = await this.client.GetCategoryByIdAsync( new() {
            Id = id
        }, new() );

        return res?.ToDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByParentId( int id ) {
        GetNavigatorCategoriesByParentIdResponse res = await this.client.GetNavigatorCategoriesByParentIdAsync( new GetNavigatorCategoriesByParentIdRequest() {
            ParentId = id
        }, new() );

        return res.Categories.Select( c => c.ToDto() );
    }

    public async Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id ) {
        GetNavNodesByCategoryIdResponse res = await this.client.GetNavNodesByCategoryIdAsync( new GetNavNodesByCategoryIdRequest() {
            Id = id
        }, new() );

        return res.Rooms.Select( room => room.ToDto() );
    }
}
