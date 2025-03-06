using Tony.Rooms.Cache;

namespace Tony.Rooms.Services;

public class RoomEntityService {
    private readonly RoomEntityCache cache;
    
    public RoomEntityService( RoomEntityCache cache ) {
        this.cache = cache;
    }
}
