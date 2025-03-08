using Microsoft.EntityFrameworkCore;
using Tony.Player.Cache;
using Tony.Player.Storage;
using Tony.Player.Storage.Entities;
using Tony.Shared;
using Tony.Shared.Events.Player;

namespace Tony.Player.Services;

public class PlayerService {
    private readonly PlayerDataCache cache;
    private readonly PlayerStorage storage;
    private readonly PublisherService pub;

    public PlayerService( PlayerDataCache cache, PlayerStorage storage, PublisherService pub ) {
        this.cache = cache;
        this.storage = storage;
        this.pub = pub;
    }

    public async Task<PlayerDto?> GetPlayer( int id ) {
        PlayerDto? player = await this.cache.GetPlayer( id );
        if( player is null ) {
            PlayerData? entity = await this.storage.PlayerData.FindAsync( id );
            if( entity is null )
                return null;

            player = this.MapEntityToDto( entity );
        }

        return player;
    }

    public async Task<PlayerDto?> GetPlayer( string username, string password ) {
        PlayerData? entity =
            await this.storage.PlayerData
            .Where( p => p.Username == username )
            .Where( p => p.Password == password )
            .FirstOrDefaultAsync();

        if( entity is null )
            return null;

        PlayerDto player = this.MapEntityToDto( entity );

        await this.cache.SavePlayer( player );

        await this.pub.Publish( new LoginEvent() {
            Audience = [ 0 ],
            Id = player.Id,
            Username = player.Username
        } );

        return player;
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
