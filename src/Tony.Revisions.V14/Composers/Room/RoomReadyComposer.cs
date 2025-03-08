using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomReadyComposer : ComposerBase {
    public string RoomModel { get; set; } = "model_a";
    public int RoomId { get; set; }

    public override short Header => 69;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( this.RoomModel );
        msg.Write( " " );
        msg.Write( this.RoomId );

        return msg;
    }
}
