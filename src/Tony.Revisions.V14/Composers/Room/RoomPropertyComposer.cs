using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomPropertyComposer : ComposerBase {
    public override short Header => 46;

    public string Property { get; set; } = "wallpaper";
    public int Value { get; set; }

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.WriteDelimiter( this.Property, this.Value, "/" );

        return msg;
    }
}
