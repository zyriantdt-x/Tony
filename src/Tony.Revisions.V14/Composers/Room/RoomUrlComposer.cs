using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Room;
internal class RoomUrlComposer : ComposerBase {
    public override short Header => 166;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( "/client/" );
        return msg;
    }
}
