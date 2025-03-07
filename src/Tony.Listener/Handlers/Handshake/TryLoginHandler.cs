using Tony.Listener.Composers.Alerts;
using Tony.Listener.Composers.Handshake;
using Tony.Listener.Composers.Player;
using Tony.Listener.Messages.Handshake;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Player;
using Tony.Listener.Tcp;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Handshake;
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
