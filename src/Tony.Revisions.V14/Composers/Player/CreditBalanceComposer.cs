using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Player;
internal class CreditBalanceComposer : ComposerBase {
    public override short Header => 6;

    public required int Credits { get; init; }

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( this.Credits + ".0" );

        return msg;
    }
}
