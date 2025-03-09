using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
using Tony.Shared.Mappers;
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

        return room.ToDto();
    }

    public async Task<RoomDataDto?> GetPlayerRoomData( int player_id ) {
        GetRoomDataResponse? room = await this.client.GetPlayerRoomDataAsync( new() {
            PlayerId = player_id
        } );

        return room.ToDto();
    }

    public async Task SetPlayerRoom( int player_id, int room_id ) => await this.client.SetPlayerRoomAsync( new() { PlayerId = player_id, RoomId = room_id } );

    public async Task<RoomModelDto?> GetRoomModelById( string id ) {
        GetRoomModelResponse? room_model = await this.client.GetRoomModelByIdAsync( new() {
            Id = id
        } );

        return room_model.ToDto();
    }
}
