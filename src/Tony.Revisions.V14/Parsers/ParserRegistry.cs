using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;
using Tony.Sdk.Revisions;

namespace Tony.Revisions.Parsers;
internal class ParserRegistry : IParserRegistry {
    private readonly ILogger<ParserRegistry> logger;
    private readonly ConcurrentDictionary<short, IParser> parsers;

    public ParserRegistry( ILogger<ParserRegistry> logger ) {
        this.logger = logger;

        this.parsers = new();
        this.RegisterParsers();
    }

    private void RegisterParsers() {
        // Get all types that implement IParser<T> and have the Header attribute
        List<Type> parser_types = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where( t => typeof( IParser ).IsAssignableFrom( t ) && t.IsClass )
                                  .ToList();

        foreach( Type parser_type in parser_types ) {
            HeaderAttribute? header_attribute = parser_type.GetCustomAttribute<HeaderAttribute>();
            if( header_attribute == null )
                continue;

            try {
                // Create an instance of the parser
                IParser? parser_instance = Activator.CreateInstance( parser_type ) as IParser;
                if( parser_instance != null ) {
                    this.parsers[ header_attribute.Header ] = parser_instance;
                    this.logger.LogInformation( $"Parser {parser_type.Name} registered for header {header_attribute.Header}." );
                }
            } catch( Exception ex ) {
                this.logger.LogError( $"Error instantiating parser {parser_type.Name}: {ex.Message}" );
            }
        }
    }

    public IParser? GetParser( short header ) {
        if( !this.parsers.TryGetValue( header, out IParser? p ) )
            return null;

        return p;
    }
}
