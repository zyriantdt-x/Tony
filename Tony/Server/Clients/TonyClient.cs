﻿using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal class TonyClient : ITonyClient {
    public string Uuid { get; }

    public required IWebSocketConnection FleckConnection { get; init; }

    public TonyClient() {
        this.Uuid = Guid.NewGuid().ToString();
    }

    public void Dispose() {
        this.FleckConnection.Close();
    }
}
