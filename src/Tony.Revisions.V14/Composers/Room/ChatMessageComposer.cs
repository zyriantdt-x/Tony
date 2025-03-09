using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;

namespace Tony.Revisions.V14.Composers.Room;
internal class ChatMessageComposer : ComposerBase {
    public override short Header => 24;

    public int InstanceId { get; set; }
    public string Message { get; set; } = "nomessage";

    public override ClientMessage Compose() {
        ClientMessage msg = base.Compose();

        msg.Write( this.InstanceId );
        msg.Write( this.Message );

        return msg;
    }
}
