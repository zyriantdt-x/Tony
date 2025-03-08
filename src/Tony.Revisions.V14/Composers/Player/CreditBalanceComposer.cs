using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Player;
public class CreditBalanceComposer : ComposerBase {
    public override short Header => 6;

    public required int Credits { get; init; }

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( this.Credits + ".0" );

        return msg;
    }
}
