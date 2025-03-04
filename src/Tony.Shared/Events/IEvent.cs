namespace Tony.Shared.Events;
public interface IEvent {
    string Event { get; }
    List<int> Audience { get; init; }
}
