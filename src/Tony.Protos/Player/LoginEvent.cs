
namespace Tony.Protos.Player;
public class LoginEvent : IEvent {
    public string Event => "login";
    public required List<int> Audience { get; init; }

    public required int Id { get; init; }
    public required string Username { get; init; }
}
