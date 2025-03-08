using DotNetty.Transport.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

IHostBuilder builder = Host.CreateDefaultBuilder( args );

//  configuration
builder.ConfigureAppConfiguration( conf => {
    conf.SetBasePath( Directory.GetCurrentDirectory() )
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
} );

builder.ConfigureServices( ( ctx, services ) => {
    services.AddOptions<ServerOptions>()
        .Bind( ctx.Configuration.GetSection( nameof( ServerOptions ) ) );
    services.AddOptions<ServiceOptions>()
        .Bind( ctx.Configuration.GetSection( nameof( ServiceOptions ) ) );

    // netty
    services.AddHostedService<TcpService>();
    services.AddSingleton<ChannelInitializer<IChannel>, TonyChannelInitialiser>();
    services.AddSingleton<ChannelHandlerAdapter, TonyChannelHandler>();

    // pubsub
    services.AddHostedService<SubscriberService>();
    services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( ctx.Configuration.GetValue<string>( "ServiceOptions:RedisServerAddress" ) ?? "localhost" ) );
    services.AddSingleton<Tony.Listener.Subscribers.Handlers.HandlerRegistry>();

    // pub sub handlers
    services.AddTransient<Tony.Listener.Subscribers.Handlers.Player.LoginHandler>();

    // tcp helpers
    services.AddSingleton<TonyClientService>();
    services.AddSingleton<ParserRegistry>();
    services.AddSingleton<HandlerRegistry>();

    // tcp handlers
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

    // logs
    services.AddLogging( logging =>
    {
        logging.ClearProviders(); // Remove default logging providers
        logging.AddSimpleConsole( options => {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        } );
    } );
} );

await builder.Build().RunAsync();