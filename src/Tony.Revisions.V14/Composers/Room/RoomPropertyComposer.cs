using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Room;
internal class RoomPropertyComposer : ComposerBase {
    public override short Header => 46;

    public string Property { get; set; } = "wallpaper";
    public int Value { get; set; }

    public override Message Compose() {
        Message msg = base.Compose();

        msg.WriteDelimiter( this.Property, this.Value, "/" );

        return msg;
    }
}
