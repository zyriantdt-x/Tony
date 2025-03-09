using Tony.Revisions.V14.Messages.Handshake;
using Tony.Revisions.V14.Composers.Alerts;
using Tony.Revisions.V14.Composers.Handshake;
using Tony.Revisions.V14.Composers.Player;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Handshake;
[Header( 4 )]
public class TryLoginHandler : IHandler<TryLoginMessage> {
    private readonly IPlayerService player_service;

    public TryLoginHandler( IPlayerService player_service ) {
        this.player_service = player_service;
    }

    public async Task Handle( ITonyClient client, TryLoginMessage message ) {
        int uid = await this.player_service.Login( message.Username.ToLower(), message.Password.ToLower() );
        if( uid < 1 ) {
            await client.SendAsync( new AlertComposer() { Message = "Username or password incorrect." } );
            return;
        }

        client.PlayerId = uid;

        await client.SendAsync( new LoginComposer() );
        await client.SendAsync( new RightsComposer() );
        ClientMessage msg = new( 229 );
        msg.Write( 0 );
        msg.Write( 0 );
        msg.Write( 0 );
        await client.SendAsync( msg );
    }
}
