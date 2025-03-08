using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomEventInfoComposer : ComposerBase {
    public override short Header => 370;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );
        msg.Write( (-1).ToString() );
        return msg;
    }
}
