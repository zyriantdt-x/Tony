using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Composers.Room;
public class RoomAdComposer : ComposerBase
{
    public override short Header => 208;

    public override Message Compose()
    {
        Message msg = base.Compose();
        msg.Write(0);
        return msg;
    }
}
