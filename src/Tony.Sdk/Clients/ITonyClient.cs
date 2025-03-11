using DotNetty.Transport.Channels;
using Tony.Sdk.Revisions;

namespace Tony.Sdk.Clients;
public interface ITonyClient {
    string Uuid { get; }
    int PlayerId { get; set; }
    IChannel Channel { get; set; }

    bool HasPonged { get; set; }

    Task SendAsync( ComposerBase msg_composer );
    Task SendAsync( ClientMessage message );
    Task SendQueued( ComposerBase msg_composer );
    Task SendQueued( ClientMessage message );
    void Flush();
}
