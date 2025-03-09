using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Listener.Services.Player;
internal class PlayerDataService {
    private readonly PlayerDataEndpoint.PlayerDataEndpointClient client;

    public PlayerDataService( IOptions<ServiceOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.PlayerServiceAddress ) );
    }

    public async Task<PlayerDto> GetUserObject( int id ) {
        UserObjectResponse res = await this.client.GetUserObjectAsync( new() {
            Id = id
        }, new() );

        return new() {
            Id = res.Id,
            Username = res.Username,
            Credits = 0,
            Figure = res.Figure,
            Sex = res.Sex == "M",
            Mission = res.Mission,
            Tickets = res.Tickets,
            PoolFigure = res.PoolFigure,
            Film = res.Film,
            ReceiveNews = res.ReceiveNews
        };
    }

    public async Task<int> GetPlayerCredits( int id ) {
        GetCreditsResponse res = await this.client.GetCreditsAsync( new() { Id = id }, new() );
        return res.Credits;
    }

    public async Task<string> GetUsernameById( int id ) {
        PlayerDto obj = await this.GetUserObject( id ); // we will make this nicer later

        return obj.Username;
    }
}
