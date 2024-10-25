using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server;
internal interface ISocketEventHandler {
    void SocketOpen( IWebSocketConnection socket );
    void SocketClose( IWebSocketConnection socket );
    void SocketMessage( IWebSocketConnection socket, string message );
}
