using Tony.Sdk.Clients;

namespace Tony.Sdk.Revisions;
public interface IParser {
    object Parse( ClientMessage message ); // Non-generic for type safety in registry
}

public interface IParser<T> : IParser {
    new T Parse( ClientMessage message ); // Strongly-typed parsing

    object IParser.Parse( ClientMessage message ) => this.Parse( message )!;
}