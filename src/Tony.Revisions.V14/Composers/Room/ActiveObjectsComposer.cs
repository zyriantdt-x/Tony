using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Room;
internal class ActiveObjectsComposer : ComposerBase {
    public override short Header => 32;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( 0 );

        return msg;
    }
}
