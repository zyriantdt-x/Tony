using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Tony.Player.Storage;
using Tony.Player.Storage.Entities;
using Tony.Protos;

namespace Tony.Player.Endpoints;

public class AuthEndpoint : Protos.AuthEndpoint.AuthEndpointBase {
    private readonly PlayerStorage storage;

    public AuthEndpoint( PlayerStorage storage ) {
        this.storage = storage;
    }

    public async override Task<LoginResponse> Login( LoginRequest request, ServerCallContext context ) {
        PlayerData? player =
            await this.storage.PlayerData
            .Where( p => p.Username == request.Username )
            .Where( p => p.Password == request.Password )
            .FirstOrDefaultAsync();
        if( player is null )
            return null;

        return new() { Id = player.Id };
    }
}
