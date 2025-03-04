using Microsoft.EntityFrameworkCore;
using Tony.Rooms.Storage;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<RoomStorage>( options => options.UseSqlite( builder.Configuration.GetValue<string>( "SqliteConnectionString" ) ?? "Data Source=C:\\etc\\tony.player.db" ) );

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet( "/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909" );

app.Run();
