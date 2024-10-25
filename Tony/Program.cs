using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tony;
using Tony.Util;

IHost host = Host.CreateDefaultBuilder()
                 .ConfigureServices( services => {
                     services.AddApplicationServices();
                 } )
                 .Build();

ITonyEnvironment application = host.Services.GetRequiredService<ITonyEnvironment>();

await application.Run();