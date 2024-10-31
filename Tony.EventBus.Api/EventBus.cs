using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Tony.EventBus.Api;

public class EventBus : IEventBus {
    private const string EVENT_BUS_ADDRESS = "ws://localhost:1233";

    private readonly ILogger<EventBus> logger;

    private readonly ConcurrentDictionary<string, List<Action<object>>> subscriptions;

    public EventBus( ILogger<EventBus> logger ) {
        this.logger = logger;

        this.subscriptions = new();

        this.ListenToBus();
    }

    private async Task ListenToBus() {
        string event_bus_address = Environment.GetEnvironmentVariable( "event_bus_address" ) ?? EVENT_BUS_ADDRESS;

        using ClientWebSocket socket = new();
        await socket.ConnectAsync( new Uri( event_bus_address ), CancellationToken.None );
        this.logger.LogInformation( $"Connected to TonyEventBus -> {event_bus_address}" );

        byte[] buffer = new byte[ 16384 ]; // buf size -> change this?

        while( socket.State == WebSocketState.Open ) {
            WebSocketReceiveResult result = await socket.ReceiveAsync( new ArraySegment<byte>( buffer ), CancellationToken.None );
            switch( result.MessageType ) {
                case WebSocketMessageType.Close:
                    await socket.CloseAsync( WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None );
                    this.logger.LogInformation( $"Disconnected from TonyEventBus" );
                    break;
                default:
                    await this.HandleBusMessage( buffer, result.Count );
                    this.logger.LogInformation( $"Message from TonyEventBus handled -> {Encoding.Default.GetString( buffer )}" );
                    break;
            }
        }
    }

    private async Task HandleBusMessage( byte[] buffer, int count ) {
        string message = Encoding.Default.GetString( buffer );

        this.logger.LogInformation( $"Event Bus Message Rcvd : {message}" );
    }

    public void Subscribe( string event_name, Action<object> handler ) {
        // check if there are already subscriptions - if so, add 'em to the list
        if( this.subscriptions.TryGetValue( event_name, out List<Action<object>>? subscriptions ) ) {
            if( subscriptions == null )
                throw new Exception(); // shouldn't be possible - assertion.

            subscriptions.Add( handler );
            return;
        }

        // no subscriptions? create a new list!
        this.subscriptions.TryAdd( event_name, new List<Action<object>> { handler } );
    }

    public Task Publish( string event_name, object message ) => throw new NotImplementedException();
}
