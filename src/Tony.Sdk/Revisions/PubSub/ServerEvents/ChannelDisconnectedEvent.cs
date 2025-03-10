namespace Tony.Sdk.Revisions.PubSub.ServerEvents;
public class ChannelDisconnectedEvent : EventBase {
    public override string Event => "channel_disconnected";

    public string ClientId { get; set; } = "noclientid";
    public int PlayerId { get; set; } = -1;
}
