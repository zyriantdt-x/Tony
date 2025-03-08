using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Alerts;
public class AlertComposer : ComposerBase {
    public override short Header => 139;

    public required string Message { get; set; }

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );
        msg.Write( this.Message );
        return msg;
    }
}
