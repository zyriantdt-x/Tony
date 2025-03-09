using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomUrlComposer : ComposerBase {
    public override short Header => 166;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );
        msg.Write( "/client/" );
        return msg;
    }
}
