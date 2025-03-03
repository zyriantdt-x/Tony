namespace Tony.Listener.Options;
internal class ServerOptions {
    public required string ListeningAddress { get; set; } = "0.0.0.0";
    public required int Port { get; set; } = 1337;
}
