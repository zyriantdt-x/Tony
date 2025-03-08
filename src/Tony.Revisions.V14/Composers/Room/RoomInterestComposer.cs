using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Room;
internal class RoomInterestComposer : ComposerBase {
    public override short Header => 258;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( 0 );
        return msg;
    }
}
