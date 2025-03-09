namespace Tony.Sdk.Revisions.PubSub;
public interface IEvent {
    string Event { get; }
    List<int> Audience { get; init; }
}
