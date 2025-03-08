using Tony.Revisions.V14.Composers.Player;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Player;
[Header( 7 )]
public class GetInfoHandler : IHandler {
    private readonly PlayerDataService player_data;

    public GetInfoHandler( PlayerDataService player_data ) {
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, object ClientMessage ) {
        if( client.PlayerId is null )
            return;

        PlayerDto uo = await this.player_data.GetUserObject( client.PlayerId ?? throw new Exception() );

        await client.SendAsync( new UserObjectComposer( uo ) );
    }
}
