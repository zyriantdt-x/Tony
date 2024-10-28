using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Tony.EventBus.Api;

public class EventBus : IEventBus {
    private const string EVENT_BUS_ADDRESS = "ws://127.0.0.1:1233";

    private readonly ILogger<EventBus> logger;

    public EventBus( ILogger<EventBus> logger ) {
        this.logger = logger;

        this.ListenToBus();
    }

    private async Task ListenToBus() {
        string event_bus_address = Environment.GetEnvironmentVariable( "event_bus_address" ) ?? EVENT_BUS_ADDRESS;

        using ClientWebSocket socket = new();
        await socket.ConnectAsync( new Uri( event_bus_address ), CancellationToken.None );

        byte[] buffer = new byte[ 16384 ]; // buf size -> change this?

        while( socket.State == WebSocketState.Open ) {
            WebSocketReceiveResult result = await socket.ReceiveAsync( new ArraySegment<byte>( buffer ), CancellationToken.None );
            switch( result.MessageType ) {
                case WebSocketMessageType.Close:
                    await socket.CloseAsync( WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None );
                    break;
                default:
                    await this.HandleBusMessage( buffer, result.Count );
                    break;
            }
        }
    }

    private async Task HandleBusMessage( byte[] buffer, int count ) {
        string message = BitConverter.ToString( buffer, 0, count );

        this.logger.LogInformation( $"Event Bus Message Rcvd : {message}" );
    }
}
