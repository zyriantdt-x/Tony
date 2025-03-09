using Grpc.Core;
using Tony.Rooms.Services;
using Tony.Shared.Dto;
using Tony.Shared.Mappers;
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

        return room.ToProtobuf();
    }

    public async override Task<GetRoomDataResponse> GetPlayerRoomData( GetPlayerRoomDataRequest request, ServerCallContext context ) {
        RoomDataDto? room = await this.room_data.GetPlayerRoomData( request.PlayerId );

        if( room is null )
            return null;

        return room.ToProtobuf();
    }

    public async override Task<SetPlayerRoomResponse> SetPlayerRoom( SetPlayerRoomRequest request, ServerCallContext context ) {
        await this.room_data.SetPlayerRoom( request.PlayerId, request.RoomId );

        return new();
    }

    public async override Task<GetRoomModelResponse> GetRoomModelById( GetRoomModelByIdRequest request, ServerCallContext context ) {
        RoomModelDto? room_model = await this.room_data.GetRoomModelById( request.Id );
        if( room_model is null )
            return null;

        return room_model.ToProtobuf();
    }
}
