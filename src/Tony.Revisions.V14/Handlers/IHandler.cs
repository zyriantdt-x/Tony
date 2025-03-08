using Tony.Listener.Tcp;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers;
internal interface IHandler {
    Task Handle( TonyClient client, object message ); // Non-generic for type safety in registry
}

internal interface IHandler<T> : IHandler {
    Task Handle( TonyClient client, T message ); // Strongly-typed parsing

    async Task IHandler.Handle( TonyClient client, object message ) => await this.Handle( client, (T)message );
}