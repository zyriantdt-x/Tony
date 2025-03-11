using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Reflection;
using Tony.Sdk.Clients;
using Tony.Sdk.Options;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Services;
using Tony.Server.Cache;
using Tony.Server.PubSub;
using Tony.Server.Repositories;
using Tony.Server.Services;
using Tony.Server.Storage;
using Tony.Server.Tcp;
using Tony.Server.Tcp.Clients;
using Tony.Server.Tcp.Registries;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.WebHost.ConfigureKestrel( optioms => {
    optioms.ListenAnyIP( 12321, listen_options => {
        listen_options.UseConnectionHandler<TonyConnectionHandler>();
    } );
} );

builder.Configuration
    .SetBasePath( Directory.GetCurrentDirectory() )
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

// options
builder.Services.AddOptions<ServerOptions>()
    .Bind( builder.Configuration.GetSection( nameof( ServerOptions ) ) );

// add redis
builder.Services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( builder.Configuration.GetValue<string>( "RedisServer" ) ?? "localhost" ) );
builder.Services.AddSingleton<PubSubHandlerRegistry>();
builder.Services.AddHostedService<SubscriberService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();

// add dotnetty
//builder.Services.AddHostedService<TcpService>();
builder.Services.AddSingleton<ITonyClientService, TonyClientService>();
//builder.Services.AddSingleton<ChannelInitializer<IChannel>, TonyChannelInitialiser>();
//builder.Services.AddSingleton<ChannelHandlerAdapter, TonyChannelHandler>();

// add registries
builder.Services.AddSingleton<HandlerRegistry>();
builder.Services.AddSingleton<ParserRegistry>();

// add ef
builder.Services.AddDbContextFactory<TonyStorage>( options => options.UseSqlite( builder.Configuration.GetValue<string>( "SqliteConnectionString" ) ?? "Data Source=C:\\etc\\tony.db" ) );

// add cache
builder.Services.AddTransient<NavigatorCache>();
builder.Services.AddTransient<PlayerCache>();
builder.Services.AddTransient<RoomDataCache>();
builder.Services.AddTransient<RoomEntityCache>();

// add repositories
builder.Services.AddTransient<NavigatorRepository>();
builder.Services.AddTransient<PlayerRepository>();
builder.Services.AddTransient<RoomDataRepository>();

// add builder.Services
builder.Services.AddTransient<INavigatorService, NavigatorService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IRoomDataService, RoomDataService>();
builder.Services.AddTransient<IRoomEntityService, RoomEntityService>();

LoadRevision( builder.Configuration[ "RevisionPath" ] ?? "C:\\etc\\Tony.Revisions.V14.dll", builder.Services );

// logs
builder.Services.AddLogging( logging => {
    logging.ClearProviders(); // Remove default logging providers
    logging.AddSimpleConsole( options => {
        options.SingleLine = true;
        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    } );
} );

await builder.Build().RunAsync();


static void LoadRevision( string path, IServiceCollection services ) {
    if( !File.Exists( path ) ) {
        Console.WriteLine( $"BOOTSTRAP: Revision not found: {path}" );
        return;
    }

    Assembly assembly = Assembly.LoadFrom( path );

    // Find a type that contains the service registration method
    Type? serviceRegistrarType = assembly.GetTypes()
        .FirstOrDefault( t => t.GetMethod( "RegisterServices", BindingFlags.Static | BindingFlags.Public ) != null );

    if( serviceRegistrarType is null ) {
        Console.WriteLine( "BOOTSTRAP: No suitable service registrar class found in revision." );
        return;
    }

    MethodInfo? registerMethod = serviceRegistrarType.GetMethod( "RegisterServices", BindingFlags.Static | BindingFlags.Public );
    if( registerMethod is null ) {
        Console.WriteLine( "BOOTSTRAP: RegisterServices method not found." );
        return;
    }

    // Invoke the static method and pass IServiceCollection
    registerMethod.Invoke( null, new object[] { services } );
}