using Microsoft.EntityFrameworkCore;
using Tony.Server.Storage;
using Tony.Sdk.Dto;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Repositories;

internal class PlayerRepository {
    private readonly TonyStorage storage;

    public PlayerRepository( IDbContextFactory<TonyStorage> storage_factory ) {
        this.storage = storage_factory.CreateDbContext();
    }

    public async Task<PlayerDto?> GetPlayerById( int id ) {
        PlayerData? player = await this.storage.PlayerData.FindAsync( id );
        if( player is null )
            return null;

        return this.MapEntityToDto( player );
    }

    public async Task<int> GetPlayerByCredentials( string username, string password ) { 
        PlayerData? player = await this.storage.PlayerData.Where(p => p.Username == username && p.Password == password ).FirstOrDefaultAsync();
        if( player is null )
            return -1;

        return player.Id;
    }

    private PlayerDto MapEntityToDto( PlayerData entity ) 
        => new() {
            Id = entity.Id,
            Username = entity.Username,
            Credits = entity.Credits,
            Figure = entity.Figure,
            Sex = entity.Sex,
            Mission = entity.Mission,
            Tickets = entity.Tickets,
            PoolFigure = entity.PoolFigure,
            Film = entity.Film,
            ReceiveNews = entity.ReceiveNews
        };
}
