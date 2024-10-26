using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server;
internal class SocketEventHandler : ISocketEventHandler {
    public async Task SocketOpen( IWebSocketConnection socket ) {

    }

    public async Task SocketClose( IWebSocketConnection socket ) {

    }

    public async Task SocketMessage( IWebSocketConnection socket, string message ) {
    
    }
}
