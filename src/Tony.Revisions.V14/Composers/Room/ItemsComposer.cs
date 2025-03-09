using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class ItemsComposer : ComposerBase {
    public override short Header => 45;

    public override ClientMessage Compose() => new( this.Header );
}
