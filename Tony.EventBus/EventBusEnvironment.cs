using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.EventBus.Server;

namespace Tony.EventBus;
internal class EventBusEnvironment : IEventBusEnvironment {
    private readonly WebSocketServer server;

    public EventBusEnvironment( WebSocketServer server ) {
        this.server = server;
    }

    public async Task Run() {
        await this.server.StartListening();

        while( true ) { }
    }
}
