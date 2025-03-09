using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Events.Player;
public class LoginEvent : EventBase {
    public override string Event => "login";

    public required int PlayerId { get; set; }
    public required string ConnectionId { get; set; }
}
