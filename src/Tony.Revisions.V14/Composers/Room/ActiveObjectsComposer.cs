using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class ActiveObjectsComposer : ComposerBase {
    public override short Header => 32;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( 0 );

        return msg;
    }
}
