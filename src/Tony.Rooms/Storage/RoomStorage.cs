using Microsoft.EntityFrameworkCore;

namespace Tony.Rooms.Storage;

public class RoomStorage : DbContext {
    public RoomStorage( DbContextOptions<RoomStorage> options ) : base( options ) { }
}
