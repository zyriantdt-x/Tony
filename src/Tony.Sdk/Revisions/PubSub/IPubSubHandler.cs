namespace Tony.Sdk.Revisions.PubSub;
public interface IPubSubHandler {
    Task Handle( IEvent message ); // Non-generic for type safety in registry
}

public interface IPubSubHandler<T> : IPubSubHandler {
    Task Handle( T message ); // Strongly-typed parsing

    async Task IPubSubHandler.Handle( IEvent message ) => await this.Handle( ( T )message );
}