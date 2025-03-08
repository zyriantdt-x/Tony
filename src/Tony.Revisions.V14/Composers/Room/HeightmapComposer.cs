using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Composers.Room;
public class HeightmapComposer : ComposerBase
{
    public override short Header => 31;

    public string Heightmap { get; set; } = String.Empty;

    public override Message Compose()
    {
        Message msg = base.Compose();
        msg.Write(this.Heightmap, true);
        return msg;
    }
}
