using Tony.Rooms.Cache;
using Tony.Rooms.Storage;
using Tony.Rooms.Storage.Entities;
using Tony.Shared.Dto;

namespace Tony.Rooms.Services;

public class RoomDataService {
    private readonly RoomStorage storage;
    private readonly RoomCache cache;

    public RoomDataService(RoomStorage storage, RoomCache cache) {
        this.storage = storage;
        this.cache = cache;
    }

    public async Task<RoomDataDto> GetRoomDataById( int id ) {
        RoomDataDto? room_data = await this.cache.GetRoomDataById( id );

        // not in redis
        if(room_data is null) {
            RoomData? room = await this.storage.RoomData.FindAsync( id );
            if( room is null )
                return null;

            room_data = new() {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                VisitorsMax = room.VisitorsMax,
                VisitorsNow = room.VisitorsNow,
                Category = room.Category,
                Ccts = room.Ccts,
                OwnerId = room.OwnerId,
                AccessType = room.AccessType
            };
            await this.cache.SaveRoomData( room_data );
        }

        return room_data;
    }
}
