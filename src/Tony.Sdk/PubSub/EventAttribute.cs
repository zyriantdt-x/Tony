namespace Tony.Sdk.PubSub;
public class EventAttribute : Attribute {
    public string EventName { get; }

    public EventAttribute( string event_name ) {
        this.EventName = event_name;
    }
}