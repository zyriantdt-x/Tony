using Microsoft.EntityFrameworkCore;
using Tony.Player.Endpoints;
using Tony.Player.Services;
using Tony.Player.Storage;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<PlayerStorage>(options => options.UseSqlite("Data Source=C:\\etc\\tony.player.db"));

builder.Services.AddScoped<PlayerDataService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthEndpoint>();
app.MapGet( "/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909" );

app.Run();
