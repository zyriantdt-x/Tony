using Tony.Sdk.Clients;

namespace Tony.Sdk.Revisions;
public interface IHandler {
    Task Handle( ITonyClient client, object message ); // Non-generic for type safety in registry
}

public interface IHandler<T> : IHandler {
    Task Handle( ITonyClient client, T message ); // Strongly-typed parsing

    async Task IHandler.Handle( ITonyClient client, object message ) => await this.Handle( client, (T)message );
}