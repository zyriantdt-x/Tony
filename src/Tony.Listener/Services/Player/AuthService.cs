using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Protos;

namespace Tony.Listener.Services.Player;
internal class AuthService {
    private readonly AuthEndpoint.AuthEndpointClient client;

    public AuthService( IOptions<PlayerOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.PlayerServiceAddress ) );
    }

    public async Task<int?> Login( string username, string password ) {
        LoginResponse res = await this.client.LoginAsync( new() {
            Username = username,
            Password = password
        }, new() );

        return res.Id == 0 ? null : res.Id;
    }
}
