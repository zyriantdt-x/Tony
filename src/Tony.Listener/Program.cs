using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Tony.Listener.Handlers;
using Tony.Listener.Handlers.Handshake;
using Tony.Listener.Handlers.Messenger;
using Tony.Listener.Handlers.Navigator;
using Tony.Listener.Handlers.Player;
using Tony.Listener.Handlers.Rooms;
using Tony.Listener.Options;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Player;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Subscribers;
using Tony.Listener.Tcp;
using Tony.Listener.Tcp.Clients;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.WebHost.ConfigureKestrel( optioms => {
    optioms.ListenAnyIP( 12321, listen_options => {
        listen_options.UseConnectionHandler<TonyConnectionHandler>();
    } );
} );

builder.Host.ConfigureAppConfiguration( ( ctx, config ) => {
    config.SetBasePath( Directory.GetCurrentDirectory() )
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
} )
    .ConfigureServices( ( ctx, services ) => {
        //services.AddHostedService<TcpService>();
        services.AddSingleton<TonyConnectionHandler>();
        services.AddConnections();

        services.AddHostedService<SubscriberService>();

        services.AddSingleton<TonyClientService>();

        services.AddSingleton<ParserRegistry>();
        services.AddSingleton<HandlerRegistry>();

        // handlers
        services.AddTransient<InitCryptoHandler>();
        services.AddTransient<GenerateKeyHandler>();
        services.AddTransient<TryLoginHandler>();

        services.AddTransient<GetInfoHandler>();
        services.AddTransient<GetCreditsHandler>();

        services.AddTransient<NavigateHandler>();

        services.AddTransient<GetRoomInfoHandler>();
        services.AddTransient<GetInterestHandler>();
        services.AddTransient<RoomDirectoryHandler>();
        services.AddTransient<TryRoomHandler>();
        services.AddTransient<GoToRoomHandler>();
        services.AddTransient<GetRoomAdHandler>();
        services.AddTransient<GetHeightmapHandler>();

        services.AddTransient<MessengerInitHandler>();

        // grpc interfaces
        services.AddTransient<AuthService>();
        services.AddTransient<PlayerDataService>();
        services.AddTransient<NavigatorService>();
        services.AddTransient<RoomDataService>();

        services.AddOptions<ServerOptions>()
            .Bind( ctx.Configuration.GetSection( nameof( ServerOptions ) ) );

        services.AddOptions<ServiceOptions>()
            .Bind( ctx.Configuration.GetSection( nameof( ServiceOptions ) ) );

        // pub sub
        services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( ctx.Configuration.GetValue<string>( "RedisServer" ) ?? "localhost" ) );
        services.AddSingleton<Tony.Listener.Subscribers.Handlers.HandlerRegistry>();

        services.AddTransient<Tony.Listener.Subscribers.Handlers.Player.LoginHandler>();
    } );

await builder.Build().RunAsync();