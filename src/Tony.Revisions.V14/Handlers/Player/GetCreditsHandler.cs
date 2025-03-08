using Tony.Revisions.Composers.Player;
using Tony.Revisions.Parsers;
using Tony.Revisions.Services.Player;
using Tony.Revisions.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Revisions.Handlers.Player;
[Header( 8 )]
internal class GetCreditsHandler : IHandler {
    private readonly PlayerDataService player_data;

    public GetCreditsHandler( PlayerDataService player_data ) {
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, object message ) {
        if( client.PlayerId is null )
            return;

        PlayerDto? player = await this.player_data.GetUserObject( client.PlayerId ?? 0 );
        if( player is null )
            return;

        await client.SendAsync( new CreditBalanceComposer() { Credits = player.Credits } );
    }
}
