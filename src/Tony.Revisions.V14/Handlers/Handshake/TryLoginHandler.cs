using Tony.Revisions.V14.Composers.Alerts;
using Tony.Revisions.V14.Composers.Handshake;
using Tony.Revisions.V14.Composers.Player;
using Tony.Revisions.V14.Messages.Handshake;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Handshake;
[Header( 4 )]
public class TryLoginHandler : IHandler<TryLoginMessage> {
    private readonly AuthService auth;

    public TryLoginHandler( AuthService auth ) {
        this.auth = auth;
    }

    public async Task Handle( TonyClient client, TryLoginMessage message ) {
        int? uid = await this.auth.Login( message.Username.ToLower(), message.Password.ToLower() );
        if( uid is null ) {
            await client.SendAsync( new AlertComposer() { Message = "Username or password incorrect." } );
            return;
        }

        client.PlayerId = uid;

        await client.SendAsync( new LoginComposer() );
        await client.SendAsync( new RightsComposer() );
        Message msg = new( 229 );
        msg.Write( 0 );
        msg.Write( 0 );
        msg.Write( 0 );
        await client.SendAsync( msg );
    }
}
