using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Navigator;
public class NavNodeInfoComposer : ComposerBase {
    public override short Header => 220;

    public required CategoryDto ParentCategory { get; set; }
    public required ICollection<CategoryDto> Subcategories { get; set; }
    public required ICollection<NavNodeDto> Rooms { get; set; }
    public required bool HideFull { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( this.HideFull );
        msg.Write( this.ParentCategory.Id );
        msg.Write( this.ParentCategory.IsPublicSpace ? 0 : 2 );
        msg.Write( this.ParentCategory.Name );
        msg.Write( 100 ); // current
        msg.Write( 100 ); // max
        msg.Write( this.ParentCategory.ParentId );

        if( !this.ParentCategory.IsPublicSpace ) {
            msg.Write( this.Rooms.Count );
        }

        foreach( NavNodeDto room in this.Rooms ) {
            if( room.IsPublicRoom ) { // public room
                int idx = 0;
                string desc = room.Description;

                if( desc.Contains( "/" ) ) {
                    string[] data = desc.Split( '/' );
                    desc = data[ 0 ];
                    idx = Convert.ToInt32( data[ 1 ] );
                }

                msg.Write( room.Id + 1000 ); // public room port
                msg.Write( 1 );
                msg.Write( room.Name );
                msg.Write( room.VisitorsNow );
                msg.Write( room.VisitorsMax );
                msg.Write( room.CategoryId );
                msg.Write( desc );
                msg.Write( room.Id );
                msg.Write( idx );
                msg.Write( room.Ccts ?? "" );
                msg.Write( 0 );
                msg.Write( 1 );
            } else {
                msg.Write( room.Id );
                msg.Write( room.Name );

                // todo: name show check
                msg.Write( room.OwnerName );

                msg.Write( room.AccessType switch {
                    AccessType.CLOSED => "closed",
                    AccessType.PASSWORD => "password",
                    _ => "open"
                } );
                msg.Write( room.VisitorsNow );
                msg.Write( room.VisitorsMax );
                msg.Write( room.Description );
            }
        }

        foreach( CategoryDto subcategory in this.Subcategories ) {
            msg.Write( subcategory.Id );
            msg.Write( 0 );
            msg.Write( subcategory.Name );
            msg.Write( 100 ); // current visitors
            msg.Write( 100 ); // max visitors
            msg.Write( subcategory.ParentId );
        }

        return msg;
    }
}
