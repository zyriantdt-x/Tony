using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Room;
internal class RoomAdComposer : ComposerBase {
    public override short Header => 208;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( 0 );
        return msg;
    }
}
