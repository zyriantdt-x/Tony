﻿namespace Tony.Listener.Options;
internal class ServiceOptions {
    public string RedisServerAddress { get; set; } = "localhost";

    public string PlayerServiceAddress { get; set; } = "http://localhost:50051";
    public string RoomServiceAddress { get; set; } = "http://localhost:50052";
}
