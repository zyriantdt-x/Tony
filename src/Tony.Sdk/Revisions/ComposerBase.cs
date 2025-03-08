using Tony.Sdk.Clients;

namespace Tony.Sdk.Revisions;
public abstract class ComposerBase {
    public abstract short Header { get; }

    public virtual ClientMessage Compose() => new ClientMessage( this.Header );
}
