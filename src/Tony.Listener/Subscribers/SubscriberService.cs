using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using Tony.Listener.Subscribers.Handlers;
using Tony.Protos;
using Tony.Protos.Player;

namespace Tony.Listener.Subscribers;
internal class SubscriberService : IHostedService {
    private readonly ILogger<SubscriberService> logger;
    private readonly ISubscriber sub;

    private readonly HandlerRegistry handler_registry;

    private readonly JsonSerializerOptions json_opts = new(); // ugh

    public SubscriberService( ILogger<SubscriberService> logger, IConnectionMultiplexer redis, HandlerRegistry handler_registry ) {
        this.logger = logger;
        this.sub = redis.GetSubscriber();

        this.handler_registry = handler_registry;

        this.json_opts.Converters.Add( new EventJsonConverter() ); // ugh
    }

    public async Task StartAsync( CancellationToken cancellation_token ) {
        this.logger.LogInformation( "test" );
        await this.sub.SubscribeAsync( "tony", async ( channel, message ) => {
            this.logger.LogInformation( $"SubscriberService message received: {message}" );

            IEvent? evt = JsonSerializer.Deserialize<IEvent>( message, this.json_opts );
            if( evt is null )
                return;

            IHandler? h = this.handler_registry.GetHandler( (( IEvent )evt).Event );
            if( h is null )
                return;

            await h.Handle( (IEvent)evt );

        } ); // will eventually make multiple queues
        this.logger.LogInformation( "SubscriberService started." );
    }
    public async Task StopAsync( CancellationToken cancellation_token ) => await this.sub.UnsubscribeAllAsync();
}
