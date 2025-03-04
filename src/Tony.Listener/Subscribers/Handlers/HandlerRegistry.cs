using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tony.Listener.Subscribers.Handlers;
internal class HandlerRegistry {
    private readonly ILogger<HandlerRegistry> logger;
    private readonly ConcurrentDictionary<string, IHandler> handlers;

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
            QueueAttribute? header_attribute = handler_type.GetCustomAttribute<QueueAttribute>();
            if( header_attribute == null )
                continue;

            try {
                // Create an instance of the parser
                IHandler? handler_instance = this.sp.GetRequiredService( handler_type ) as IHandler;
                if( handler_instance != null ) {
                    this.handlers[ header_attribute.QueueName ] = handler_instance;
                    this.logger.LogInformation( $"Handler {handler_type.Name} registered for event {header_attribute.QueueName}." );
                }
            } catch( Exception ex ) {
                this.logger.LogError( $"Error instantiating parser {handler_type.Name}: {ex.Message}" );
            }
        }
    }

    public IHandler? GetHandler( string queue_name ) {
        if( !this.handlers.TryGetValue( queue_name, out IHandler p ) )
            return null;

        return p;
    }
}
