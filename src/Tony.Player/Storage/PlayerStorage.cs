using Microsoft.EntityFrameworkCore;
using Tony.Player.Storage.Entities;

namespace Tony.Player.Storage;

public class PlayerStorage : DbContext {
    public DbSet<PlayerData> PlayerData { get; set; }

    public PlayerStorage( DbContextOptions<PlayerStorage> options ) : base( options ) { }
}
