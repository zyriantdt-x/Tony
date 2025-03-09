using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Shared.Mappers;
public static class RoomDataMapper {
    public static RoomDataDto ToDto( this GetRoomDataResponse room ) => new() {
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

    public static GetRoomDataResponse ToProtobuf( this RoomDataDto room ) => new() {
        Id = room.Id,
        Name = room.Name,
        Description = room.Description,
        VisitorsMax = room.VisitorsMax,
        VisitorsNow = room.VisitorsNow,
        Category = room.Category,
        Ccts = room.Ccts,
        OwnerId = room.OwnerId,
        Accesstype = room.AccessType,
        Model = room.Model
    };
}
