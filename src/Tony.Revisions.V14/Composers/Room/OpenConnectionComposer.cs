using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class OpenConnectionComposer : ComposerBase {
    public override short Header => 19;

    public override ClientMessage Compose() => new ClientMessage( this.Header );
}
