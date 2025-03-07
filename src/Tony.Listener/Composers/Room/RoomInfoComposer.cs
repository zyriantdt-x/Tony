﻿using Tony.Listener.Tcp;
using Tony.Shared.Dto;

namespace Tony.Listener.Composers.Room;
internal class RoomInfoComposer : ComposerBase {
    public override short Header => 54;

    public required RoomDataDto RoomData { get; set; }
    public required string OwnerUsername { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();

        msg.Write( this.RoomData.SuperUsers );
        msg.Write( this.RoomData.AccessType switch {
            AccessType.CLOSED => "closed",
            AccessType.PASSWORD => "password",
            _ => "open"
        } );
        msg.Write( this.RoomData.Id );

        // todo: allow owner hiding
        msg.Write( this.OwnerUsername );

        msg.Write( this.RoomData.Model );
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
