using Grpc.Core;
using Tony.Player.Dto;
using Tony.Player.Services;
using Tony.Shared.Protos;

namespace Tony.Player.Endpoints;

public class AuthEndpoint : Shared.Protos.AuthEndpoint.AuthEndpointBase {
    private readonly PlayerService player;

    public AuthEndpoint( PlayerService player ) {
        this.player = player;
    }

    public async override Task<LoginResponse> Login( LoginRequest request, ServerCallContext context ) {
        PlayerDto? player = await this.player.GetPlayer( request.Username, request.Password );
        if( player is null )
            return null;

        return new() { Id = player.Id };
    }
}
