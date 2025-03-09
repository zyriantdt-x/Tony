using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Server.PubSub;
internal class PubSubHandlerRegistry {
    private readonly ILogger<PubSubHandlerRegistry> logger;
    private readonly ConcurrentDictionary<string, IPubSubHandler> handlers;

    //ugh
    private readonly IServiceProvider sp;

    public PubSubHandlerRegistry( ILogger<PubSubHandlerRegistry> logger, IServiceProvider sp ) {
        this.logger = logger;
        this.sp = sp;

        this.handlers = new();
        this.RegisterHandlers();
    }

    private void RegisterHandlers() {
        // Get all types that implement IParser<T> and have the Header attribute
        List<Type> parser_types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany( assembly => assembly.GetTypes() )
            .Where( t => typeof( IPubSubHandler ).IsAssignableFrom( t ) && t.IsClass )
            .ToList();

        foreach( Type handler_type in parser_types ) {
            EventAttribute? header_attribute = handler_type.GetCustomAttribute<EventAttribute>();
            if( header_attribute == null )
                continue;

            try {
                // Create an instance of the parser
                IPubSubHandler? handler_instance = this.sp.GetRequiredService( handler_type ) as IPubSubHandler;
                if( handler_instance != null ) {
                    this.handlers[ header_attribute.EventName ] = handler_instance;
                    this.logger.LogInformation( $"Handler {handler_type.Name} registered for event {header_attribute.EventName}." );
                }
            } catch( Exception ex ) {
                this.logger.LogError( $"Error instantiating parser {handler_type.Name}: {ex.Message}" );
            }
        }
    }

    public IPubSubHandler? GetHandler( string queue_name ) {
        if( !this.handlers.TryGetValue( queue_name, out IPubSubHandler? p ) )
            return null;

        return p;
    }
}
