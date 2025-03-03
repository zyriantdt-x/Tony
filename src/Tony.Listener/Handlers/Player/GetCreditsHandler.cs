using Tony.Listener.Composers.Player;
using Tony.Listener.Messages.Player;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Player;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Player;
[Header( 8 )]
internal class GetCreditsHandler : IHandler<GetCreditsMessage> {
    private readonly PlayerDataService player_data;

    public GetCreditsHandler(PlayerDataService player_data ) {
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, GetCreditsMessage message ) {
        if( client.PlayerId is null )
            return;

        int credits = await this.player_data.GetPlayerCredits( client.PlayerId ?? throw new Exception() );
        await client.SendAsync( new CreditBalanceComposer() { Credits = credits } );
    }
}
