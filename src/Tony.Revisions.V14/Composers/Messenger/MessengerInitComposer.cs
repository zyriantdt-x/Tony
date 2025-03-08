using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Messenger;
public class MessengerInitComposer : ComposerBase {
    public override short Header => 12;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( "test msg" );
        msg.Write( 100 );
        msg.Write( 100 );
        msg.Write( 100 );
        msg.Write( 0 );

        return msg;
    }
}
