using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomLetInComposer : ComposerBase {
    public override short Header => 41;

    public override ClientMessage Compose() => new( this.Header );
}
