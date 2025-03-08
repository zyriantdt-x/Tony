using Tony.Revisions.Composers.Player;
using Tony.Revisions.Parsers;
using Tony.Revisions.Services.Player;
using Tony.Revisions.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Revisions.Handlers.Player;
[Header( 7 )]
internal class GetInfoHandler : IHandler {
    private readonly PlayerDataService player_data;

    public GetInfoHandler( PlayerDataService player_data ) {
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, object message ) {
        if( client.PlayerId is null )
            return;

        PlayerDto uo = await this.player_data.GetUserObject( client.PlayerId ?? throw new Exception() );

        await client.SendAsync( new UserObjectComposer( uo ) );
    }
}
