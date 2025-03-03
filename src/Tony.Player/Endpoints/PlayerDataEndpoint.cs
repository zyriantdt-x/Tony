using Grpc.Core;
using Tony.Player.Storage;
using Tony.Player.Storage.Entities;
using Tony.Protos;

namespace Tony.Player.Endpoints;

public class PlayerDataEndpoint : Protos.PlayerDataEndpoint.PlayerDataEndpointBase {
    private readonly PlayerStorage storage;

    public PlayerDataEndpoint( PlayerStorage storage ) {
        this.storage = storage;
    }

    public async override Task<UserObjectResponse> GetUserObject( UserObjectRequest request, ServerCallContext context ) {
        PlayerData? player = await this.storage.PlayerData.FindAsync( request.Id );
        if( player is null )
            return null;

        return new() {
            Id = player.Id,
            Username = player.Username,
            Figure = player.Figure,
            Sex = player.Sex ? "m" : "f",
            Mission = player.Mission,
            Tickets = player.Tickets,
            PoolFigure = player.PoolFigure,
            Film = player.Film,
            ReceiveNews = player.ReceiveNews
        };
    }
}
