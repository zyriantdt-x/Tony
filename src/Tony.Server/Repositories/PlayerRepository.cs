using Microsoft.EntityFrameworkCore;
using Tony.Server.Storage;
using Tony.Sdk.Dto;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Repositories;

internal class PlayerRepository {
    private readonly IDbContextFactory<TonyStorage> storage;

    public PlayerRepository( IDbContextFactory<TonyStorage> storage_factory ) {
        this.storage = storage_factory;
    }

    public async Task<PlayerDto?> GetPlayerById( int id ) {
        using TonyStorage storage_ctx = await this.storage.CreateDbContextAsync();
        PlayerData? player = await storage_ctx.PlayerData.FindAsync( id );
        if( player is null )
            return null;

        return this.MapEntityToDto( player );
    }

    public async Task<int> GetPlayerByCredentials( string username, string password ) {
        using TonyStorage storage_ctx = await this.storage.CreateDbContextAsync();
        PlayerData? player = await storage_ctx.PlayerData.Where(p => p.Username == username && p.Password == password ).FirstOrDefaultAsync();
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
