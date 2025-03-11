using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;

namespace Tony.Revisions.V14.Handlers.Handshake;
[Header( 196 )]
internal class PongHandler : IHandler {
    public Task Handle( ITonyClient client, object message ) {
        client.HasPonged = true;
        return Task.CompletedTask;
    }
}
