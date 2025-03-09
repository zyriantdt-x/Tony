using Tony.Sdk.Dto;

namespace Tony.Sdk.Services;
public interface IRoomEntityService {
    Task<IEnumerable<RoomEntityDto>> GetEntitiesInRoom( int room_id );
    Task AddEntityToRoom( int room_id, RoomEntityDto entity );
    Task RemoveEntityFromRoom( int room_id, int instance_id );

}
