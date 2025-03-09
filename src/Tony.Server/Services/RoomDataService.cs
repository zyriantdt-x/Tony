using Tony.Sdk.Dto;
using Tony.Sdk.Services;
using Tony.Server.Cache;
using Tony.Server.Repositories;

namespace Tony.Server.Services;
internal class RoomDataService : IRoomDataService {
    private readonly RoomDataRepository repository;
    private readonly RoomDataCache cache;

    public RoomDataService( RoomDataRepository repository, RoomDataCache cache ) {
        this.repository = repository;
        this.cache = cache;
    }

    public async Task<RoomDataDto?> GetRoomDataById( int id ) {
        RoomDataDto? room = await this.cache.GetRoomDataById( id ) ?? await this.repository.GetRoomDataById( id );
        if(room is not null)
            await this.cache.SaveRoomData( room );

        return room;
    }
}
