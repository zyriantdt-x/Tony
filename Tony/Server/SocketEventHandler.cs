using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Server.Clients;

namespace Tony.Server;
internal class SocketEventHandler : ISocketEventHandler {
    private readonly ILogger<SocketEventHandler> logger;
    private readonly ITonyClientService client_service;
    private readonly IMessageSender message_sender;

    public SocketEventHandler( ILogger<SocketEventHandler> logger,
                               ITonyClientService client_service,
                               IMessageSender message_sender ) {
        this.logger = logger;
        this.client_service = client_service;
        this.message_sender = message_sender;
    }

    public async Task SocketOpen( IWebSocketConnection socket ) {
        ITonyClient client = await this.client_service.CreateAndRegisterClient( socket );

        await this.message_sender.SendAsync( client, "open" );

        this.logger.LogInformation( $"Socket opened -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketClose( IWebSocketConnection socket ) {
        this.logger.LogInformation( $"Socket closed -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketMessage( IWebSocketConnection socket, string message ) {
        this.logger.LogInformation( $"Socket sent message -> {socket.ConnectionInfo.ClientIpAddress} says {message}" );
    }
}
