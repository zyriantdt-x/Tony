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

    public async Task<RoomDataDto?> GetRoomDataById( int id ) {
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
                AccessType = room.AccessType,
                Model = room.ModelId
            };
            await this.cache.SaveRoomData( room_data );
        }

        return room_data;
    }

    public async Task<RoomModelDto?> GetRoomModelById( string id ) {
        RoomModelDto? room_model = await this.cache.GetRoomModelById( id );

        // not in redis
        if( room_model is null ) {
            RoomModel? model = await this.storage.RoomModels.FindAsync( id );
            if( model is null )
                return null;

            room_model = new() {
                Id = model.Id,
                DoorX = model.DoorX,
                DoorY = model.DoorY,
                DoorZ = model.DoorZ,
                DoorDir = model.DoorDir,
                Heightmap = model.Heightmap,
                TriggerClass = model.TriggerClass,
            };
            await this.cache.SaveRoomModel( room_model );
        }

        return room_model;
    }

    public async Task<RoomDataDto?> GetPlayerRoomData( int id ) => await this.cache.GetPlayerRoomData( id );
    public async Task SetPlayerRoom( int player_id, int room_id ) => await this.cache.SetPlayerRoom( player_id, room_id );
}
