using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
using Tony.Shared.Mappers;
using Tony.Shared.Protos;

namespace Tony.Listener.Services.Rooms; 
internal class RoomEntityService {
    private readonly RoomEntityEndpoint.RoomEntityEndpointClient client;

    public RoomEntityService( IOptions<ServiceOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.RoomServiceAddress ) );
    }

    public async Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int id ) {
        GetEntitiesInRoomResponse res = await this.client.GetEntitiesInRoomAsync( new() {
            RoomId = id
        } );

        return res.Entities.Select( entity => entity.ToDto() );
    }

    public async Task AddEntityToRoom( int room_id, RoomEntityDto entity ) {
        await this.client.AddEntityToRoomAsync( new() {
            RoomId = room_id,
            Entity = entity.ToProto()
        } );
    }

    public async Task RemoveEntityFromRoom( int room_id, int instance_id ) {
        await this.client.RemoveEntityFromRoomAsync( new() {
            RoomId = room_id,
            InstanceId = instance_id
        } );
    }
}
