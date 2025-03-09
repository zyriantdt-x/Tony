using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Events.Player;
public class LoginEvent : EventBase {
    public override string Event => "login";

    public required int Id { get; init; }
    public required string Username { get; init; }
}
