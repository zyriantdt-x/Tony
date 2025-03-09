using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Tony.Rooms.Cache;
using Tony.Rooms.Endpoints;
using Tony.Rooms.Services;
using Tony.Rooms.Storage;
using Tony.Shared;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );
builder.WebHost.ConfigureKestrel( options => {
    options.ConfigureEndpointDefaults( lo => lo.Protocols = HttpProtocols.Http2 );
} );

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<RoomStorage>( options => options.UseSqlite( builder.Configuration.GetValue<string>( "SqliteConnectionString" ) ?? "Data Source=C:\\etc\\tony.rooms.db" ) );
builder.Services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( builder.Configuration.GetValue<string>( "RedisServer" ) ?? "localhost" ) );

builder.Services.AddScoped<NavigatorService>();
builder.Services.AddScoped<NavigatorCache>();

builder.Services.AddScoped<RoomCache>();
builder.Services.AddScoped<RoomDataService>();

builder.Services.AddScoped<RoomEntityService>();
builder.Services.AddScoped<RoomEntityCache>();

builder.Services.AddScoped<PublisherService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<NavigatorEndpoint>();
app.MapGrpcService<RoomDataEndpoint>();
app.MapGrpcService<RoomEntityEndpoint>();
app.MapGrpcReflectionService();
app.MapGet( "/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909" );

app.Run();
