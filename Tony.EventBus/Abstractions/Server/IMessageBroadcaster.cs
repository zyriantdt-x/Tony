using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus.Server;
internal interface IMessageBroadcaster {
    Task RegisterListener( IWebSocketConnection listener );
    Task DeregisterListener( IWebSocketConnection listener );

    Task BroadcastMessage( string message );
}
