using Microsoft.EntityFrameworkCore;
using Tony.Rooms.Storage.Entities;

namespace Tony.Rooms.Storage;

public class RoomStorage : DbContext {
    public DbSet<Category> Categories { get; set; }
    public DbSet<RoomData> RoomData { get; set; }
    public DbSet<RoomModel> RoomModels { get; set; }

    public RoomStorage( DbContextOptions<RoomStorage> options ) : base( options ) { }

    protected override void OnModelCreating( ModelBuilder mb ) {
        mb.Entity<RoomData>()
            .HasOne( room => room.Model )
            .WithOne()
            .HasForeignKey<RoomData>( room => room.ModelId );
    }
}
