using Tony.Sdk.Dto;
using Tony.Sdk.Enums;

namespace Tony.Sdk.Services;
public interface IRoomEntityService {
    Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int room_id );
    Task AddEntityToRoom( RoomEntityDto entity );
    Task RemoveEntityFromRoom( RoomEntityDto entity );
    Task EntityChat( RoomEntityDto entity, ChatType chat_type, string message );
    Task<RoomEntityDto?> GetPlayerEntity( PlayerRoomDto player_room );
}
