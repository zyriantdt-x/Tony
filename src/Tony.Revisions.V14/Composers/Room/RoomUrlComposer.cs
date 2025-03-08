using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Composers.Room;
public class RoomUrlComposer : ComposerBase
{
    public override short Header => 166;

    public override Message Compose()
    {
        Message msg = base.Compose();
        msg.Write("/client/");
        return msg;
    }
}
