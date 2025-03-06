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
            AccessType = room.Accesstype,
            Model = room.Model,
        };
    }

    public async Task<RoomDataDto?> GetPlayerRoomData( int player_id ) {
        GetRoomDataResponse? room = await this.client.GetPlayerRoomDataAsync( new() {
            PlayerId = player_id
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
            AccessType = room.Accesstype,
            Model = room.Model
        };
    }

    public async Task<RoomModelDto?> GetRoomModelById( string id ) {
        GetRoomModelResponse? room_model = await this.client.GetRoomModelByIdAsync( new() {
            Id = id
        } );

        return room_model is null ? null : new() {
            Id = room_model.Id,
            DoorX = room_model.DoorX,
            DoorY = room_model.DoorY,
            DoorZ = room_model.DoorZ,
            DoorDir = room_model.DoorDir,
            Heightmap = room_model.Heightmap,
            TriggerClass = room_model.TriggerClass,
        };
    }
}
