using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomAdComposer : ComposerBase {
    public override short Header => 208;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );
        msg.Write( 0 );
        return msg;
    }
}
