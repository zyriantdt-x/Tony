using Tony.Sdk.Clients;

namespace Tony.Sdk.Revisions;
public abstract class ComposerBase {
    public abstract short Header { get; }

    public abstract IClientMessage Compose();
}
