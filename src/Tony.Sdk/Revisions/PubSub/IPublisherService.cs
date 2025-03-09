namespace Tony.Sdk.Revisions.PubSub;
public interface IPublisherService {

    Task Publish<T>( T evt );
}
