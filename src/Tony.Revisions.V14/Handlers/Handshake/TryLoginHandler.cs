using Tony.Revisions.V14.ClientMessages.Handshake;
using Tony.Revisions.V14.Composers.Alerts;
using Tony.Revisions.V14.Composers.Handshake;
using Tony.Revisions.V14.Composers.Player;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Handshake;
[Header( 4 )]
public class TryLoginHandler : IHandler<TryLoginClientMessage> {
    private readonly AuthService auth;

    public TryLoginHandler( AuthService auth ) {
        this.auth = auth;
    }

    public async Task Handle( TonyClient client, TryLoginClientMessage ClientMessage ) {
        int? uid = await this.auth.Login( ClientMessage.Username.ToLower(), ClientMessage.Password.ToLower() );
        if( uid is null ) {
            await client.SendAsync( new AlertComposer() { ClientMessage = "Username or password incorrect." } );
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
