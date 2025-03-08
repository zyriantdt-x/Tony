using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Room;
class UserObjectsComposer : ComposerBase {
    public override short Header => 28;

    public required IEnumerable<RoomEntityDto> Entities { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();

        foreach( RoomEntityDto entity in this.Entities ) {
            msg.Write( "\r", true );
            switch( entity.EntityType ) {
                case EntityType.PET: break;
                case EntityType.PLAYER:
                case EntityType.BOT:
                    msg.WriteKeyValue( "i", entity.InstanceId );
                    msg.WriteKeyValue( "a", entity.EntityId );
                    msg.WriteKeyValue( "n", entity.Username );
                    msg.WriteKeyValue( "f", entity.Figure );
                    msg.WriteKeyValue( "s", entity.Sex );
                    msg.WriteKeyValue( "l", $"{entity.PosX} {entity.PosY} {entity.PosZ.ToString( "0.0" )}" );
                    msg.WriteKeyValue( "c", entity.Motto );
                    break;
            }
        }

        return msg;
    }
}
