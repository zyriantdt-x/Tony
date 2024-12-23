﻿using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal interface ITonyClient {
    string Uuid { get; }
    IWebSocketConnection FleckConnection { get; init; }

    void Dispose();
}
