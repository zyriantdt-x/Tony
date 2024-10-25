using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Server;

namespace Tony;
internal class TonyEnvironment : ITonyEnvironment {
    private readonly ILogger<TonyEnvironment> logger;
    private readonly IWebSocketServer server;

    public TonyEnvironment( ILogger<TonyEnvironment> logger,
                            IWebSocketServer server ) {
        this.logger = logger;
        this.server = server;
    }

    public async Task Run() {
        await this.server.StartListening();

        while (true) { }
    }
}
