using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
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

        return res.Entities.Select( entity =>  new RoomEntityDto {
            InstanceId = entity.InstanceId,
            EntityId = entity.EntityId,
            EntityType = ( EntityType )entity.EntityType,
            Username = entity.Username,
            Figure = entity.Figure,
            Sex = entity.Sex,
            Motto = entity.Motto,
            Badge = entity.Badge,
            PosX = entity.PosX,
            PosY = entity.PosY,
            PosZ = entity.PosZ
        } );
    }

    public async Task AddEntityToRoom( int room_id, RoomEntityDto entity ) {
        await this.client.AddEntityToRoomAsync( new() {
            RoomId = room_id,
            Entity = new() {
                InstanceId = entity.InstanceId,
                EntityId = entity.EntityId,
                EntityType = ( int )entity.EntityType,
                Username = entity.Username,
                Figure = entity.Figure,
                Sex = entity.Sex,
                Motto = entity.Motto,
                Badge = entity.Badge,
                PosX = entity.PosX,
                PosY = entity.PosY,
                PosZ = entity.PosZ
            }
        } );
    }

    public async Task RemoveEntityFromRoom( int room_id, int instance_id ) {
        await this.client.RemoveEntityFromRoomAsync( new() {
            RoomId = room_id,
            InstanceId = instance_id
        } );
    }
}
