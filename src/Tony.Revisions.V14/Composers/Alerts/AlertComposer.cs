using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Alerts;
internal class AlertComposer : ComposerBase {
    public override short Header => 139;

    public required string Message { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( this.Message );
        return msg;
    }
}
