namespace Tony.Sdk.PubSub;
public interface IPublisherService {

    Task Publish<T>( T evt );
}
