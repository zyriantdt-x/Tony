using Tony.Listener.Tcp;

namespace Tony.Listener.Composers;
internal abstract class ComposerBase {
    public abstract short Header { get; }

    public virtual Message Compose() => new( this.Header );
}
