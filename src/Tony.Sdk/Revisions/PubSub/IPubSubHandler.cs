namespace Tony.Sdk.Revisions.PubSub;
public interface IPubSubHandler {
    Task Handle( EventBase message ); // Non-generic for type safety in registry
}

public interface IPubSubHandler<T> : IPubSubHandler where T : EventBase {
    Task Handle( T message ); // Strongly-typed parsing

    async Task IPubSubHandler.Handle( EventBase message ) => await this.Handle( ( T )message );
}