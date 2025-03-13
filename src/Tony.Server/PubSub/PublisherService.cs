using StackExchange.Redis;
using System.Text.Json;
using Tony.Sdk.PubSub;

namespace Tony.Server.PubSub;
internal class PublisherService : IPublisherService {
    private const string QUEUE_NAME = "tony";

    private readonly IConnectionMultiplexer redis;

    public PublisherService( IConnectionMultiplexer redis ) {
        this.redis = redis;
    }

    public async Task Publish<T>( T evt ) {
        ISubscriber sub = this.redis.GetSubscriber();
        await sub.PublishAsync( QUEUE_NAME, JsonSerializer.Serialize( evt ) );
    }
}
