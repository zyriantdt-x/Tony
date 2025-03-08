using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Composers.Room;
public class UpdateVotesComposer : ComposerBase
{
    public override short Header => 345;

    public int Rating { get; set; } = 0;

    public override Message Compose()
    {
        Message msg = base.Compose();
        msg.Write(this.Rating);
        return msg;
    }
}
