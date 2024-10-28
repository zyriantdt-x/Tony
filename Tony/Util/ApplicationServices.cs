using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.EventBus.Api;
using Tony.Server;
using Tony.Server.Clients;

namespace Tony.Util;
internal static class ApplicationServices {
    public static void AddApplicationServices( this IServiceCollection services ) {
        services.AddSingleton<ITonyEnvironment, TonyEnvironment>();

        services.AddSingleton<IWebSocketServer, WebSocketServer>();
        services.AddSingleton<ISocketEventHandler, SocketEventHandler>();

        services.AddSingleton<ITonyClientService, TonyClientService>();
        services.AddSingleton<IMessageSender, MessageSender>();

        services.AddSingleton<IEventBus, Tony.EventBus.Api.EventBus>();
    }
}
