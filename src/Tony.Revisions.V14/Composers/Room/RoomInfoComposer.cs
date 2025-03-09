using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
public class RoomInfoComposer : ComposerBase {
    public override short Header => 54;

    public required RoomDataDto RoomData { get; set; }

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( this.RoomData.SuperUsers );
        msg.Write( this.RoomData.AccessType switch {
            AccessType.CLOSED => "closed",
            AccessType.PASSWORD => "password",
            _ => "open"
        } );
        msg.Write( this.RoomData.Id );

        // todo: allow owner hiding
        msg.Write( this.RoomData.OwnerName );

        msg.Write( this.RoomData.ModelId );
        msg.Write( this.RoomData.Name );
        msg.Write( this.RoomData.Description );

        msg.Write( true ); // show owner name
        msg.Write( true ); // allow trading
        msg.Write( false ); // ?

        msg.Write( this.RoomData.VisitorsNow );
        msg.Write( this.RoomData.VisitorsMax );

        return msg;
    }
}
