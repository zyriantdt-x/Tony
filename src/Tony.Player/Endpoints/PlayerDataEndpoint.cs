using Grpc.Core;
using Tony.Player.Services;
using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Player.Endpoints;

public class PlayerDataEndpoint : Shared.Protos.PlayerDataEndpoint.PlayerDataEndpointBase {
    private readonly PlayerService player;

    public PlayerDataEndpoint( PlayerService player ) {
        this.player = player;
    }

    public async override Task<UserObjectResponse> GetUserObject( UserObjectRequest request, ServerCallContext context ) {
        PlayerDto? player = await this.player.GetPlayer( request.Id );
        if( player is null )
            return null;

        return new() {
            Id = player.Id,
            Username = player.Username,
            Credits = player.Credits,
            Figure = player.Figure,
            Sex = player.Sex ? "M" : "F",
            Mission = player.Mission,
            Tickets = player.Tickets,
            PoolFigure = player.PoolFigure,
            Film = player.Film,
            ReceiveNews = player.ReceiveNews
        };
    }
}
