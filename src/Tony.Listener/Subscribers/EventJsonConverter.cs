using System.Text.Json.Serialization;
using System.Text.Json;
using Tony.Protos;
using Tony.Protos.Player;

// this is so fucking ugly but it works
internal class EventJsonConverter : JsonConverter<IEvent> {
    private static readonly Dictionary<string, Type> event_map = new() {
        { "login", typeof(LoginEvent) },
        // Add more events here...
    };

    public override IEvent Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) {
        using JsonDocument doc = JsonDocument.ParseValue( ref reader );

        JsonElement root_element = doc.RootElement;
        string? event_type = root_element.GetProperty( "Event" ).GetString() ?? throw new JsonException( "No Event element" );
        Type? event_type_class = event_map[ event_type ] ?? throw new JsonException( $"Unknown event type: {event_type}" );

        return JsonSerializer.Deserialize( root_element.GetRawText(), event_type_class, options ) as IEvent ?? throw new JsonException();
    }

    public override void Write( Utf8JsonWriter writer, IEvent value, JsonSerializerOptions options ) => JsonSerializer.Serialize( writer, value, value.GetType(), options );
}