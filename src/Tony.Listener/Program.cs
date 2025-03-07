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

// configuration
builder.Configuration.SetBasePath( Directory.GetCurrentDirectory() )
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
builder.Services.AddOptions<ServerOptions>()
    .Bind( builder.Configuration.GetSection( nameof( ServerOptions ) ) );
builder.Services.AddOptions<ServiceOptions>()
    .Bind( builder.Configuration.GetSection( nameof( ServiceOptions ) ) );

// kestrel services
builder.Services.AddConnections();
builder.Services.AddSingleton<TonyConnectionHandler>();

// pubsub
builder.Services.AddHostedService<SubscriberService>();
builder.Services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( builder.Configuration.GetValue<string>( "RedisServer" ) ?? "localhost" ) );
builder.Services.AddSingleton<Tony.Listener.Subscribers.Handlers.HandlerRegistry>();

// pub sub handlers
builder.Services.AddTransient<Tony.Listener.Subscribers.Handlers.Player.LoginHandler>();

// tcp helpers
builder.Services.AddSingleton<TonyClientService>();
builder.Services.AddSingleton<ParserRegistry>();
builder.Services.AddSingleton<HandlerRegistry>();

// tcp handlers
builder.Services.AddTransient<InitCryptoHandler>();
builder.Services.AddTransient<GenerateKeyHandler>();
builder.Services.AddTransient<TryLoginHandler>();

builder.Services.AddTransient<GetInfoHandler>();
builder.Services.AddTransient<GetCreditsHandler>();

builder.Services.AddTransient<NavigateHandler>();

builder.Services.AddTransient<GetRoomInfoHandler>();
builder.Services.AddTransient<GetInterestHandler>();
builder.Services.AddTransient<RoomDirectoryHandler>();
builder.Services.AddTransient<TryRoomHandler>();
builder.Services.AddTransient<GoToRoomHandler>();
builder.Services.AddTransient<GetRoomAdHandler>();
builder.Services.AddTransient<GetHeightmapHandler>();

builder.Services.AddTransient<MessengerInitHandler>();

// grpc interfaces
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<PlayerDataService>();
builder.Services.AddTransient<NavigatorService>();
builder.Services.AddTransient<RoomDataService>();

await builder.Build().RunAsync();