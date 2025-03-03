using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Handshake;
internal class CryptoParametersComposer : ComposerBase {
    public override short Header => 277;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( 0 );

        return msg;
    }
}
