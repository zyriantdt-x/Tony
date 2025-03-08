using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Room;
internal class ObjectsWorldComposer : ComposerBase {
    public override short Header => 30;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( 0 );

        return msg;
    }
}
