using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Room;
internal class RoomUrlComposer : ComposerBase {
    public override short Header => 166;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( "/client/" );
        return msg;
    }
}
