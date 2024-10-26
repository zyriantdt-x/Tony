using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal interface ITonyClientService {
    Task<ITonyClient> CreateAndRegisterClient( IWebSocketConnection socket );

    Task<ITonyClient?> GetClient( IWebSocketConnection socket );
}
