using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Alerts;
public class AlertComposer : ComposerBase {
    public override short Header => 139;

    public required string Message { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( this.Message );
        return msg;
    }
}
