using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server;
internal class SocketEventHandler : ISocketEventHandler {
    private readonly ILogger<SocketEventHandler> logger;

    public SocketEventHandler( ILogger<SocketEventHandler> logger ) {
        this.logger = logger;
    }

    public async Task SocketOpen( IWebSocketConnection socket ) {
        this.logger.LogInformation( $"Socket opened -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketClose( IWebSocketConnection socket ) {
        this.logger.LogInformation( $"Socket closed -> {socket.ConnectionInfo.ClientIpAddress}" );
    }

    public async Task SocketMessage( IWebSocketConnection socket, string message ) {
        this.logger.LogInformation( $"Socket sent message -> {socket.ConnectionInfo.ClientIpAddress} says {message}" );
    }
}
