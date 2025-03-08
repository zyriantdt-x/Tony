using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Composers.Room;
public class ActiveObjectsComposer : ComposerBase
{
    public override short Header => 32;

    public override Message Compose()
    {
        Message msg = base.Compose();

        msg.Write(0);

        return msg;
    }
}
