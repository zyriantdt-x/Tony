using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Shared.Mappers;
public static class NavNodeMapper {
    public static NavNodeDto ToDto( this NavNode room ) => new() {
        Id = room.Id,
        IsPublicRoom = false,
        Name = room.Name,
        Description = room.Description,
        VisitorsMax = room.VisitorsMax,
        VisitorsNow = room.VisitorsNow,
        CategoryId = room.CategoryId,
        Ccts = room.Ccts,
        OwnerName = room.OwnerName,
        AccessType = ( AccessType )room.AccessType 
    };

    public static NavNode ToProtobuf( this NavNodeDto room ) => new() {
        Id = room.Id,
        IsPublicRoom = false,
        Name = room.Name,
        Description = room.Description,
        VisitorsMax = room.VisitorsMax,
        VisitorsNow = room.VisitorsNow,
        CategoryId = room.CategoryId,
        Ccts = room.Ccts,
        OwnerName = room.OwnerName,
        AccessType = ( int )room.AccessType
    };
}
