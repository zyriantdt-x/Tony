using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Listener.Services.Rooms;
internal class RoomDataService {
    private readonly RoomDataEndpoint.RoomDataEndpointClient client;

    public RoomDataService( IOptions<ServiceOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.RoomServiceAddress ) );
    }

    public async Task<RoomDataDto?> GetRoomDataById( int id ) {
        GetRoomDataResponse? room = await this.client.GetRoomDataByIdAsync( new() {
            Id = id
        } );

        return room is null ? null : new() {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            Category = room.Category,
            Ccts = room.Ccts,
            OwnerId = room.OwnerId,
            AccessType = room.Accesstype
        };
    }
}
