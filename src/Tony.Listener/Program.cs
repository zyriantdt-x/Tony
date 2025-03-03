using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tony.Listener.Handlers;
using Tony.Listener.Handlers.Handshake;
using Tony.Listener.Options;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Player;
using Tony.Listener.Tcp;
using Tony.Listener.Tcp.Clients;

IHost host = Host.CreateDefaultBuilder( args )
    .ConfigureAppConfiguration( ( ctx, config ) => {
        config.SetBasePath( Directory.GetCurrentDirectory() )
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();
    } )
    .ConfigureServices( ( ctx, services ) => {
        services.AddHostedService<TcpService>();

        services.AddSingleton<TcpClientHandler>();
        services.AddSingleton<TonyClientService>();

        services.AddSingleton<ParserRegistry>();
        services.AddSingleton<HandlerRegistry>();

        // handlers
        services.AddTransient<InitCryptoHandler>();
        services.AddTransient<GenerateKeyHandler>();
        services.AddTransient<TryLoginHandler>();

        // grpc interfaces
        services.AddTransient<AuthService>();

        services.AddOptions<ServerOptions>()
            .Bind( ctx.Configuration.GetSection( nameof( ServerOptions ) ) );
    } )
    .Build();

await host.RunAsync();