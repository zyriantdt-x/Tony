namespace Tony.Protos;
public interface IEvent {
    string Event { get; }
    List<int> Audience { get; init; }
}
