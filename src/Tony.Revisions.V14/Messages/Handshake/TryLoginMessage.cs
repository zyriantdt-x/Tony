namespace Tony.Revisions.V14.Messages.Handshake;
public class TryLoginClientMessage {
    public required string Username { get; init; }
    public required string Password { get; init; }
}
