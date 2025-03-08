using Grpc.Core;
using Tony.Rooms.Services;
using Tony.Shared.Dto;
using Tony.Shared.Protos;
using static Grpc.Core.Metadata;

namespace Tony.Rooms.Endpoints;

public class RoomEntityEndpoint : Shared.Protos.RoomEntityEndpoint.RoomEntityEndpointBase {
    private readonly RoomEntityService entity_service;

    public RoomEntityEndpoint( RoomEntityService entity_service ) {
        this.entity_service = entity_service;
    }
    public async override Task<GetEntitiesInRoomResponse> GetEntitiesInRoom( GetEntitiesInRoomRequest request, ServerCallContext context ) {
        ICollection<RoomEntityDto> entities = await this.entity_service.GetEntitiesInRoom( request.RoomId );

        GetEntitiesInRoomResponse res = new();
        res.Entities.AddRange( entities.Select( entity => new RoomEntityProto() {
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
        } ) );

        return res;
    }

    public async override Task<AddEntityToRoomResponse> AddEntityToRoom( AddEntityToRoomRequest request, ServerCallContext context ) {
        RoomEntityProto entity = request.Entity;

        await this.entity_service.AddEntityToRoom( request.RoomId, new() {
            InstanceId = entity.InstanceId,
            EntityId = entity.EntityId,
            EntityType = ( EntityType )entity.EntityType ,
            Username = entity.Username,
            Figure = entity.Figure,
            Sex = entity.Sex,
            Motto = entity.Motto,
            Badge = entity.Badge,
            PosX = entity.PosX,
            PosY = entity.PosY,
            PosZ = entity.PosZ
        } );

        return new();
    }

    public async override Task<RemoveEntityFromRoomResponse> RemoveEntityFromRoom( RemoveEntityFromRoomRequest request, ServerCallContext context ) {
        await this.entity_service.RemoveEntityFromRoom( request.RoomId, request.InstanceId );

        return new();
    }
}
