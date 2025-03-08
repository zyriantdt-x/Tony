using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Handshake;
internal class CryptoParametersComposer : ComposerBase {
    public override short Header => 277;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( 0 );

        return msg;
    }
}
