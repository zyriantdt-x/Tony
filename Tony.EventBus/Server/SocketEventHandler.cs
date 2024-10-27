using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus.Server;
internal class SocketEventHandler : ISocketEventHandler {
    private readonly ILogger<SocketEventHandler> logger;
    private readonly IMessageBroadcaster message_broadcaster;

    public SocketEventHandler( ILogger<SocketEventHandler> logger,
                               IMessageBroadcaster message_broadcaster ) {
        this.logger = logger;
        this.message_broadcaster = message_broadcaster;
    }

    public async Task SocketOpen( IWebSocketConnection socket ) {
        await this.message_broadcaster.RegisterListener( socket );
        this.logger.LogInformation( $"Socket opened -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketClose( IWebSocketConnection socket ) {
        await this.message_broadcaster.DeregisterListener( socket );
        this.logger.LogInformation( $"Socket closed -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketMessage( IWebSocketConnection socket, string message ) {
        await this.message_broadcaster.BroadcastMessage( message );
        this.logger.LogInformation( $"Socket sent message -> {socket.ConnectionInfo.ClientIpAddress} says {message}" );
    }
}
