using Tony.Revisions.V14.Composers.Player;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Player;
[Header( 8 )]
public class GetCreditsHandler : IHandler {
    private readonly IPlayerService player_service;

    public GetCreditsHandler( IPlayerService player_service ) {
        this.player_service = player_service;
    }

    public async Task Handle( ITonyClient client, object ClientMessage ) {
        if( client.PlayerId < 1 )
            return;

        PlayerDto? player = await this.player_service.GetPlayerById( client.PlayerId );
        if( player is null )
            return;

        await client.SendAsync( new CreditBalanceComposer() { Credits = player.Credits } );
    }
}
