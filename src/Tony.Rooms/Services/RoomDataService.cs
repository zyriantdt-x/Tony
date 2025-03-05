using Tony.Rooms.Storage;

namespace Tony.Rooms.Services;

public class RoomDataService {
    private readonly RoomStorage storage;

    public RoomDataService(RoomStorage storage) {
        this.storage = storage;
    }
}
