namespace Tony.Sdk.PubSub;
public abstract class EventBase {
    public abstract string Event { get; }
    public IEnumerable<int> Audience { get; set; } = [];
}
