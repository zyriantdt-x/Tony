﻿using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.Sockets;
using Tony.Listener.Composers;

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

    public async Task SendToAll( ComposerBase msg_composer ) {
        foreach( TonyClient client in this.connected_clients ) { 
            await client.SendAsync( msg_composer );
        }
    }

    public async Task SendToMany( List<int> player_ids, ComposerBase msg_composer ) {
        IEnumerable<TonyClient> clients = this.connected_clients.Where( client => client.PlayerId is not null && player_ids.Contains( client.PlayerId ?? 0 ) );
        foreach( TonyClient client in clients ) {
            await client.SendAsync( msg_composer );
        }
    }
}
