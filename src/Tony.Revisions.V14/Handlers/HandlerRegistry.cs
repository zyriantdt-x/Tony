using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;
using Tony.Listener.Parsers;
using Tony.Sdk.Revisions;

namespace Tony.Revisions.V14.Handlers;
internal class HandlerRegistry : IHandlerRegistry {
    private readonly ILogger<HandlerRegistry> logger;
    private readonly ConcurrentDictionary<short, IHandler> handlers;

    //ugh
    private readonly IServiceProvider sp;

    public HandlerRegistry( ILogger<HandlerRegistry> logger, IServiceProvider sp ) {
        this.logger = logger;
        this.sp = sp;

        this.handlers = new();
        this.RegisterHandlers();
    }

    private void RegisterHandlers() {
        // Get all types that implement IParser<T> and have the Header attribute
        List<Type> parser_types = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where( t => typeof( IHandler ).IsAssignableFrom( t ) && t.IsClass )
                                  .ToList();

        foreach( Type handler_type in parser_types ) {
            HeaderAttribute? header_attribute = handler_type.GetCustomAttribute<HeaderAttribute>();
            if( header_attribute == null )
                continue;

            try {
                // Create an instance of the parser
                IHandler? handler_instance = this.sp.GetRequiredService( handler_type ) as IHandler;
                if( handler_instance != null ) {
                    this.handlers[ header_attribute.Header ] = handler_instance;
                    this.logger.LogInformation( $"Handler {handler_type.Name} registered for header {header_attribute.Header}." );
                }
            } catch( Exception ex ) {
                this.logger.LogError( $"Error instantiating parser {handler_type.Name}: {ex.Message}" );
            }
        }
    }

    public IHandler? GetHandler( short header ) {
        if( !this.handlers.TryGetValue( header, out IHandler? p ) )
            return null;

        return p;
    }
}
