using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Player;
public class CreditBalanceComposer : ComposerBase {
    public override short Header => 6;

    public required int Credits { get; init; }

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( this.Credits + ".0" );

        return msg;
    }
}
