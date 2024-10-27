using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.EventBus.Server;

namespace Tony.EventBus.Util;
internal static class ApplicationServices {
    public static void AddApplicationServices( this IServiceCollection services ) { 
        services.AddSingleton<IEventBusEnvironment, EventBusEnvironment>();

        services.AddSingleton<WebSocketServer>();

        services.AddSingleton<ISocketEventHandler, SocketEventHandler>();
        services.AddSingleton<IMessageBroadcaster, MessageBroadcaster>();
    }
}
