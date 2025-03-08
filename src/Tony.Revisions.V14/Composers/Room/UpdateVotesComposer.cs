using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class UpdateVotesComposer : ComposerBase {
    public override short Header => 345;

    public int Rating { get; set; } = 0;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );
        msg.Write( this.Rating );
        return msg;
    }
}
