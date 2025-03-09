using Tony.Shared.Dto;
using Tony.Shared.Protos;
using static Grpc.Core.Metadata;

namespace Tony.Shared.Mappers;
public static class RoomEntityMapper {
    public static RoomEntityDto ToDto( this RoomEntityProto entity ) => new() {
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
    };

    public static RoomEntityProto ToProto( this RoomEntityDto entity ) => new() {
        InstanceId = entity.InstanceId,
        EntityId = entity.EntityId,
        EntityType = ( int )entity.EntityType ,
        Username = entity.Username,
        Figure = entity.Figure,
        Sex = entity.Sex,
        Motto = entity.Motto,
        Badge = entity.Badge,
        PosX = entity.PosX,
        PosY = entity.PosY,
        PosZ = entity.PosZ
    };
}
