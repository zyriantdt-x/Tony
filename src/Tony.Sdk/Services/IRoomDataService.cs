using Tony.Sdk.Dto;

namespace Tony.Sdk.Services; 
public interface IRoomDataService {
    Task<RoomDataDto?> GetRoomDataById( int id );
}
