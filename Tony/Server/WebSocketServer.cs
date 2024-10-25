using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// convenience /shrug
using LogLevel = Fleck.LogLevel;

namespace Tony.Server;
internal class WebSocketServer : IWebSocketServer {
    private const string ADDRESS = "ws://0.0.0.0:1232";

    private readonly ILogger<WebSocketServer> logger;

    private Fleck.IWebSocketServer fleck_server;

    public WebSocketServer( ILogger<WebSocketServer> logger ) {
        this.logger = logger;

        this.fleck_server = new Fleck.WebSocketServer( ADDRESS );

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
    }

    public async Task StartListening() {
        this.fleck_server.Start( socket => {
            socket.OnOpen = () => {

            };
        } );

        this.logger.LogInformation( "tony server started" );
    }
}
