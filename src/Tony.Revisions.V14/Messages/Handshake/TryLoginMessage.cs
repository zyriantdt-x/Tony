using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Messages.Handshake;
public class TryLoginMessage
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
