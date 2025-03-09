namespace Tony.Sdk.Revisions.PubSub.ServerEvents;
public class ChannelConnectedEvent : EventBase {
    public override string Event => "channel_connected";

    public string ClientId { get; set; } = "noclientid";
}
