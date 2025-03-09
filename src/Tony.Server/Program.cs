using DotNetty.Transport.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Tony.Sdk.Options;
using Tony.Sdk.Services;
using Tony.Server.Cache;
using Tony.Server.Repositories;
using Tony.Server.Services;
using Tony.Server.Storage;
using Tony.Server.Tcp;

IHostBuilder builder = Host.CreateDefaultBuilder( args );

//  configuration
builder.ConfigureAppConfiguration( conf => {
    conf.SetBasePath( Directory.GetCurrentDirectory() )
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
} );

builder.ConfigureServices( ( ctx, services ) => {
    // options
    services.AddOptions<ServerOptions>()
        .Bind( ctx.Configuration.GetSection( nameof( ServerOptions ) ) );

    // add dotnetty
    services.AddHostedService<TcpService>();
    services.AddSingleton<ChannelInitializer<IChannel>, TonyChannelInitialiser>();
    services.AddSingleton<ChannelHandlerAdapter, TonyChannelHandler>();

    // add ef
    services.AddDbContextFactory<TonyStorage>( options => options.UseSqlite( ctx.Configuration.GetValue<string>( "SqliteConnectionString" ) ?? "Data Source=C:\\etc\\tony.db" ) );

    // add cache
    services.AddTransient<NavigatorCache>();
    services.AddTransient<PlayerCache>();
    services.AddTransient<RoomDataCache>();

    // add repositories
    services.AddTransient<NavigatorRepository>();
    services.AddTransient<PlayerRepository>();
    services.AddTransient<RoomDataRepository>();

    // add services
    services.AddTransient<INavigatorService, NavigatorService>();
    services.AddTransient<IPlayerService, PlayerService>();
    services.AddTransient<IRoomDataService, RoomDataService>();

    LoadRevision( ctx.Configuration[ "RevisionPath" ] ?? "C:\\etc\\Tony.Revisions.V14.dll", services );

    // logs
    services.AddLogging( logging => {
        logging.ClearProviders(); // Remove default logging providers
        logging.AddSimpleConsole( options => {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        } );
    } );
} );

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