using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Tony.Player.Cache;
using Tony.Player.Endpoints;
using Tony.Player.Services;
using Tony.Player.Storage;
using Tony.Shared;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<PlayerStorage>(options => options.UseSqlite( builder.Configuration.GetValue<string>( "SqliteConnectionString" ) ?? "Data Source=C:\\etc\\tony.player.db"));
builder.Services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( builder.Configuration.GetValue<string>("RedisServer") ?? "localhost" ) );

builder.Services.AddScoped<PlayerDataCache>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<PublisherService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthEndpoint>();
app.MapGrpcService<PlayerDataEndpoint>();
app.MapGet( "/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909" );

app.Run();
