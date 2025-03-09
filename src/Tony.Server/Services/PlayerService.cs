using Tony.Sdk.Dto;
using Tony.Sdk.Services;
using Tony.Server.Cache;
using Tony.Server.Repositories;

namespace Tony.Server.Services;
internal class PlayerService : IPlayerService {
    private readonly PlayerRepository repository;
    private readonly PlayerCache cache;

    public PlayerService( PlayerRepository repository, PlayerCache cache ) {
        this.repository = repository;
        this.cache = cache;
    }

    public async Task<PlayerDto?> GetPlayerById( int id ) {
        PlayerDto? player = await this.cache.GetPlayer( id );
        if( player is null ) { 
            player = await this.repository.GetPlayerById( id );

            await this.cache.SavePlayer( player );
        }


        return player;
    }

    public async Task<int> Login( string username, string password ) {
        int player_id = await this.repository.GetPlayerByCredentials( username, password );

        return player_id;
    }
}
