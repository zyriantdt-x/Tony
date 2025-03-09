using Tony.Sdk.Dto;

namespace Tony.Sdk.Services; 
public interface IPlayerService {
    Task<PlayerDto?> GetPlayerById( int id );
    Task<int> Login( string username, string password );

    Task<PlayerRoomDto?> GetPlayerRoom( int id );
    Task SetPlayerRoom( int id, PlayerRoomDto? dto = null );
}
