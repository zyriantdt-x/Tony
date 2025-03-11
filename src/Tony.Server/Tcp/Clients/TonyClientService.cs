using Microsoft.Extensions.Logging;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;

namespace Tony.Server.Tcp.Clients;
internal class TonyClientService : ITonyClientService {
    private readonly ILogger<TonyClientService> logger;

    private readonly List<ITonyClient> connected_clients;

    public TonyClientService( ILogger<TonyClientService> logger ) {
        this.logger = logger;

        this.connected_clients = new();
    }

    public void RegisterClient( ITonyClient client ) {
        this.connected_clients.Add( client );
    }

    public void DeregisterClient( ITonyClient client ) {
        this.connected_clients.Remove( client );
    }

    public ITonyClient? GetClient( string uuid ) => this.connected_clients.FirstOrDefault( client => client.Uuid == uuid );
    public ITonyClient? GetClient( int player_id ) => this.connected_clients.FirstOrDefault( client => client.PlayerId == player_id );

    public async Task SendToAll( ComposerBase msg_composer ) {
        // todo: compose once
        foreach( TonyClient client in this.connected_clients ) {
            await client.SendAsync( msg_composer );
        }
    }

    public async Task SendToMany( IEnumerable<int> player_ids, ComposerBase msg_composer ) {
        IEnumerable<ITonyClient> clients = this.connected_clients.Where( client => player_ids.Contains( client.PlayerId ) );
        // todo: compose once
        foreach( TonyClient client in clients ) {
            await client.SendAsync( msg_composer );
        }
    }
}
