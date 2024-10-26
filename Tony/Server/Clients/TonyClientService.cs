using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal class TonyClientService : ITonyClientService {
    private readonly ILogger<TonyClientService> logger;

    private readonly List<ITonyClient> clients;

    public TonyClientService( ILogger<TonyClientService> logger ) {
        this.logger = logger;

        this.clients = new List<ITonyClient>();
    }

    public async Task<ITonyClient> CreateAndRegisterClient( IWebSocketConnection socket ) {
        // can move this into a factory later
        ITonyClient client = new TonyClient() {
            FleckConnection = socket,
        };

        this.logger.LogInformation( $"TonyClient created -> {socket.ConnectionInfo.ClientIpAddress}" );

        await this.RegisterClient( client );

        return client;
    }

    public async Task<ITonyClient?> GetClient( IWebSocketConnection socket )
        => this.clients.Where( client => client.FleckConnection == socket ).FirstOrDefault();

    public async Task DestroyClient( ITonyClient client ) {
        await this.DeregisterClient( client );

        client.Dispose();

        this.logger.LogInformation( $"TonyClient destroyed -> {client.FleckConnection.ConnectionInfo.ClientIpAddress}" );
    }

    private async Task RegisterClient( ITonyClient client ) {
        this.clients.Add( client );

        this.logger.LogInformation( $"TonyClient registered -> {client.Uuid}@{client.FleckConnection.ConnectionInfo.ClientIpAddress}" );
    }

    private async Task DeregisterClient( ITonyClient client ) {
        if( !this.clients.Remove( client ) )
            throw new Exception( "Attempt to remove non-existant client" );

        this.logger.LogInformation( $"TonyClient de-registered -> {client.Uuid}@{client.FleckConnection.ConnectionInfo.ClientIpAddress}" );
    }
}
