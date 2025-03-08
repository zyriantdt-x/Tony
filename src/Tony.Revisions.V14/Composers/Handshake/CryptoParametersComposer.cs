using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Handshake;
public class CryptoParametersComposer : ComposerBase {
    public override short Header => 277;

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( 0 );

        return msg;
    }
}
