using Microsoft.EntityFrameworkCore;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Storage;
internal class TonyStorage : DbContext {
    public required DbSet<PlayerData> PlayerData { get; set; }
    public required DbSet<NavigatorCategory> NavigatorCategories { get; set; }

    public TonyStorage( DbContextOptions<TonyStorage> options ) : base( options ) { }
}
