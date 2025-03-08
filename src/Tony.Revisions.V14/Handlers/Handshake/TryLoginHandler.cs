using Tony.Revisions.Composers.Alerts;
using Tony.Revisions.Composers.Handshake;
using Tony.Revisions.Composers.Player;
using Tony.Revisions.Messages.Handshake;
using Tony.Revisions.Parsers;
using Tony.Revisions.Services.Player;
using Tony.Revisions.Tcp;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Handshake;
[Header( 4 )]
internal class TryLoginHandler : IHandler<TryLoginMessage> {
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
