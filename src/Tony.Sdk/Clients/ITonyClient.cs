using DotNetty.Transport.Channels;
using Tony.Sdk.Composers;

namespace Tony.Sdk.Clients;
public interface ITonyClient {
    string Uuid { get; }
    int? PlayerId { get; set; }
    IChannel Channel { get; set; }

    Task SendAsync( ComposerBase msg_composer );
    Task SendAsync( IClientMessage message );
    Task SendQueued( ComposerBase msg_composer );
    Task SendQueued( IClientMessage message );
    void Flush();
}
