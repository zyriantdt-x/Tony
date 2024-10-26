using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server;
internal interface ISocketEventHandler {
    Task SocketOpen( IWebSocketConnection socket );
    Task SocketClose( IWebSocketConnection socket );
    Task SocketMessage( IWebSocketConnection socket, string message );
}
