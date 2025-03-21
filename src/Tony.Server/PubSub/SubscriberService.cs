﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tony.Sdk.PubSub;

namespace Tony.Server.PubSub;
internal class SubscriberService : IHostedService {
    private readonly ILogger<SubscriberService> logger;
    private readonly ISubscriber sub;

    private readonly PubSubHandlerRegistry handler_registry;

    private readonly JsonSerializerOptions json_opts = new(); // ugh

    public SubscriberService( ILogger<SubscriberService> logger, IConnectionMultiplexer redis, PubSubHandlerRegistry handler_registry ) {
        this.logger = logger;
        this.sub = redis.GetSubscriber();

        this.handler_registry = handler_registry;

        this.json_opts.Converters.Add( new EventJsonConverter() ); // ugh
    }

    public async Task StartAsync( CancellationToken cancellation_token ) {
        await this.sub.SubscribeAsync( "tony", async ( channel, message ) => {
            this.logger.LogInformation( $"SubscriberService message received: {message}" );

            EventBase? evt = JsonSerializer.Deserialize<EventBase>( message!, this.json_opts );
            if( evt is null ) {
                this.logger.LogError( $"Failed to parse incoming message: {message}" );
                return;
            }

            IPubSubHandler? h = this.handler_registry.GetHandler( evt.Event );
            if( h is null ) {
                this.logger.LogError( $"Failed to find handler for incoming message: {message}" );
                return;
            }

            await h.Handle( ( EventBase )evt );

        } ); // will eventually make multiple queues
        this.logger.LogInformation( "SubscriberService started." );
    }

    public async Task StopAsync( CancellationToken cancellation_token ) => await this.sub.UnsubscribeAllAsync();
}

// this is so fucking ugly but it works
internal class EventJsonConverter : JsonConverter<EventBase> {
    private static readonly Dictionary<string, Type> event_map = LoadEventMap();

    private static Dictionary<string, Type> LoadEventMap() {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany( assembly => assembly.GetTypes() )
            .Where( t => typeof( EventBase ).IsAssignableFrom( t ) && !t.IsAbstract ) // Ensure it's a subclass of EventBase and not abstract
            .Select( t => {
                // Create an instance of the type and get the Event property
                var instance = Activator.CreateInstance( t ) as EventBase;
                var event_name = instance?.Event; // Get the non-static Event property
                return new { Type = t, EventName = event_name };
            } )
            .Where( x => x.EventName != null ) // Only include types that have a valid Event name
            .ToDictionary( x => x.EventName!, x => x.Type );
    }

    public override EventBase Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options ) {
        using JsonDocument doc = JsonDocument.ParseValue( ref reader );

        JsonElement root_element = doc.RootElement;
        string? event_type = root_element.GetProperty( "Event" ).GetString() ?? throw new JsonException( "No Event element" );
        Type? event_type_class = event_map[ event_type ] ?? throw new JsonException( $"Unknown event type: {event_type}" );

        return JsonSerializer.Deserialize( root_element.GetRawText(), event_type_class, options ) as EventBase ?? throw new JsonException();
    }

    public override void Write( Utf8JsonWriter writer, EventBase value, JsonSerializerOptions options ) => JsonSerializer.Serialize( writer, value, value.GetType(), options );
}