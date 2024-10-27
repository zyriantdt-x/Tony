using Fleck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus.Server;
internal class MessageBroadcaster : IMessageBroadcaster {
    private readonly ILogger<MessageBroadcaster> logger;

    private readonly List<IWebSocketConnection> listeners;

    public MessageBroadcaster( ILogger<MessageBroadcaster> logger ) {
        this.logger = logger;

        this.listeners = new List<IWebSocketConnection>();
    }

    public async Task RegisterListener( IWebSocketConnection listener ) {
        this.listeners.Add( listener );
        // todo - api keys/auth ??
        this.logger.LogInformation( $"Listener registered -> {listener.ConnectionInfo.ClientIpAddress}:{listener.ConnectionInfo.ClientPort}" );
    }
    
    public async Task DeregisterListener( IWebSocketConnection listener ) {
        this.listeners.Remove( listener );

        this.logger.LogInformation( $"Listener de-registered -> {listener.ConnectionInfo.ClientIpAddress}:{listener.ConnectionInfo.ClientPort}" );
    }

    public async Task BroadcastMessage( string message ) {
        Parallel.ForEach( this.listeners, listener => {
            listener.Send( message );
        } );

        this.logger.LogInformation( $"Message broadcast -> {message}" );
    }
}
