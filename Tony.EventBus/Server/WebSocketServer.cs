using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLevel = Fleck.LogLevel;

namespace Tony.EventBus.Server;
internal class WebSocketServer {
    private const string ADDRESS = "ws://0.0.0.0:1233";

    private readonly ILogger<WebSocketServer> logger;
    private readonly ISocketEventHandler socket_event_handler;

    private Fleck.IWebSocketServer fleck_server;

    public WebSocketServer( ILogger<WebSocketServer> logger,
                            ISocketEventHandler socket_event_handler ) {
        this.logger = logger;
        this.socket_event_handler = socket_event_handler;

        string address = Environment.GetEnvironmentVariable( "ws_address" ) ?? ADDRESS;

        this.fleck_server = new Fleck.WebSocketServer( address );

        // configure fleck to use microsoft logging
        FleckLog.LogAction = ( level, message, ex ) => {
            switch( level ) {
                case LogLevel.Debug:
                    this.logger.LogDebug( message, ex );
                    break;
                case LogLevel.Error:
                    this.logger.LogError( message, ex );
                    break;
                case LogLevel.Warn:
                    this.logger.LogWarning( message, ex );
                    break;
                default:
                    this.logger.LogInformation( message, ex );
                    break;
            }
        };

        // will remove this later on
        FleckLog.Level = LogLevel.Debug;
    }

    public async Task StartListening() {
        this.fleck_server.Start( socket => {
            socket.OnOpen = async () => await this.socket_event_handler.SocketOpen( socket );

            socket.OnClose = async () => await this.socket_event_handler.SocketClose( socket );

            socket.OnMessage = async ( message ) => await this.socket_event_handler.SocketMessage( socket, message );
        } );

        this.logger.LogInformation( "Tony EventBus Started" );
    }
}
