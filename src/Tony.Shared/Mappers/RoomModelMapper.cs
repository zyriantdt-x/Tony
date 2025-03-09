using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Shared.Mappers;
public static class RoomModelMapper {
    public static RoomModelDto ToDto( this GetRoomModelResponse room_model ) => new() {
        Id = room_model.Id,
        DoorX = room_model.DoorX,
        DoorY = room_model.DoorY,
        DoorZ = room_model.DoorZ,
        DoorDir = room_model.DoorDir,
        Heightmap = room_model.Heightmap,
        TriggerClass = room_model.TriggerClass,
    };

    public static GetRoomModelResponse ToProtobuf( this RoomModelDto room_model ) => new() {
        Id = room_model.Id,
        DoorX = room_model.DoorX,
        DoorY = room_model.DoorY,
        DoorZ = room_model.DoorZ,
        DoorDir = room_model.DoorDir,
        Heightmap = room_model.Heightmap,
        TriggerClass = room_model.TriggerClass,
    };
}
