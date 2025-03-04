using Tony.Shared.Events;

internal interface IHandler {
    Task Handle( IEvent message ); // Non-generic for type safety in registry
}

internal interface IHandler<T> : IHandler {
    Task Handle( T message ); // Strongly-typed parsing

    async Task IHandler.Handle( IEvent message ) => await this.Handle( ( T )message );
}