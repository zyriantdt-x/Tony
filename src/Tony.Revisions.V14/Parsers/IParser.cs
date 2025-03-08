using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers;
internal interface IParser {
    object Parse( Message message ); // Non-generic for type safety in registry
}

internal interface IParser<T> : IParser {
    new T Parse( Message message ); // Strongly-typed parsing

    object IParser.Parse( Message message ) => this.Parse( message )!;
}