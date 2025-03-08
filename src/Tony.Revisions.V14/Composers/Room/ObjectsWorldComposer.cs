using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class ObjectsWorldComposer : ComposerBase {
    public override short Header => 30;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( 0 );

        return msg;
    }
}
