using Tony.Sdk.Clients;

namespace Tony.Sdk.Revisions;
public interface IParser {
    object Parse( IClientMessage message ); // Non-generic for type safety in registry
}

public interface IParser<T> : IParser {
    new T Parse( IClientMessage message ); // Strongly-typed parsing

    object IParser.Parse( IClientMessage message ) => this.Parse( message )!;
}