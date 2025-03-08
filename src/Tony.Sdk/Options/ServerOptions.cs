namespace Tony.Sdk.Options;
public class ServerOptions {
    public required int Port { get; set; } = 12321;

    public required string RedisAddress { get; set; } = "localhost";
}
