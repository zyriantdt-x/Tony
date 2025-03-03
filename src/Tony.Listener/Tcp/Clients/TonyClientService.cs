using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Tony.Listener.Tcp.Clients;
internal class TonyClientService {
    private readonly ILogger<TonyClientService> logger;

    private readonly List<TonyClient> connected_clients;

    public TonyClientService( ILogger<TonyClientService> logger ) {
        this.logger = logger;
        this.connected_clients = new();
    }

    public void RegisterClient( TonyClient client ) => this.connected_clients.Add( client );

    public void DeregisterClient( TonyClient client ) => this.connected_clients.Remove( client ); 

    public TonyClient? GetClient( string uuid ) => this.connected_clients.FirstOrDefault(client => client.Uuid == uuid);
}
