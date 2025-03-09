using Microsoft.EntityFrameworkCore;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Storage;
internal class TonyStorage : DbContext {
    public required DbSet<PlayerData> PlayerData { get; set; }
    public required DbSet<NavigatorCategory> NavigatorCategories { get; set; }
    public required DbSet<RoomData> RoomData { get; set; }
    public required DbSet<RoomModel> RoomModels { get; set; }

    public TonyStorage( DbContextOptions<TonyStorage> options ) : base( options ) { }

    protected override void OnModelCreating( ModelBuilder model_builder ) {
        model_builder.Entity<RoomData>()
            .HasOne( r => r.Owner )
            .WithMany()
            .HasForeignKey( r => r.OwnerId );

        model_builder.Entity<RoomData>()
            .HasOne( r => r.Model )
            .WithMany()
            .HasForeignKey( r => r.ModelId );
    }
}
