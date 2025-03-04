using Microsoft.EntityFrameworkCore;
using Tony.Rooms.Storage.Entities;

namespace Tony.Rooms.Storage;

public class RoomStorage : DbContext {
    public DbSet<Category> Categories { get; set; }

    public RoomStorage( DbContextOptions<RoomStorage> options ) : base( options ) { }
}
