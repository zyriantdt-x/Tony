using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tony.EventBus;
using Tony.EventBus.Util;

IHost host = Host.CreateDefaultBuilder()
                 .ConfigureServices( services => {
                     services.AddApplicationServices();
                 } )
                 .Build();

IEventBusEnvironment application = host.Services.GetRequiredService<IEventBusEnvironment>();

await application.Run();