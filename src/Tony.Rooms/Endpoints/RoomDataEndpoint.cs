using Grpc.Core;
using Tony.Rooms.Services;
using Tony.Rooms.Storage;
using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Rooms.Endpoints;

public class RoomDataEndpoint : Shared.Protos.RoomDataEndpoint.RoomDataEndpointBase {
    private readonly RoomDataService room_data;

    public RoomDataEndpoint( RoomDataService room_data ) {
        this.room_data = room_data;
    }

    public async override Task<GetRoomDataResponse> GetRoomDataById( GetRoomDataByIdRequest request, ServerCallContext context ) {
        RoomDataDto? room = await this.room_data.GetRoomDataById( request.Id );
        if( room is null )
            return null;

        return new() {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            Category = room.Category,
            Ccts = room.Ccts,
            OwnerId = 1,
            Accesstype = 1
        };
    }
}
