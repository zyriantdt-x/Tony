using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Messenger;
public class MessengerInitComposer : ComposerBase {
    public override short Header => 12;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( "test msg" );
        msg.Write( 100 );
        msg.Write( 100 );
        msg.Write( 100 );
        msg.Write( 0 );

        return msg;
    }
}
