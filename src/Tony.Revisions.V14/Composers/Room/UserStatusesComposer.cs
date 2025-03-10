using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Sdk.Extensions;
using Tony.Sdk.Revisions;

namespace Tony.Revisions.V14.Composers.Room;
internal class UserStatusesComposer : ComposerBase {
    public override short Header => 34;

    public IEnumerable<RoomEntityDto> Entities { get; set; } = [];

    public override ClientMessage Compose() {
        ClientMessage msg = base.Compose();

        foreach( RoomEntityDto entity in this.Entities ) {
            msg.WriteDelimiter( entity.InstanceId, ' ' );
            msg.WriteDelimiter( entity.PosX, ',' );
            msg.WriteDelimiter( entity.PosY, ',' );
            msg.WriteDelimiter( entity.PosZ.ToString( "0.0" ), ',' );
            msg.WriteDelimiter( entity.HeadRotation, ',' );
            msg.WriteDelimiter( entity.BodyRotation, '/' );

            foreach( KeyValuePair<EntityStatusType, string> status in entity.Statuses ) {
                msg.Write( status.Key.GetStatusCode(), true );
                
                if( !String.IsNullOrEmpty( status.Value ) ) {
                    msg.Write( " ", true );
                    msg.Write( status.Value, true );
                }

                msg.Write( "/", true );
            }

            msg.Write( 13, true );
        }

        return msg;
    }
}
