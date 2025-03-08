using DotNetty.Transport.Channels;
using System.IO.Pipelines;
using System.Net.Sockets;
using Tony.Listener.Composers;
using Tony.Listener.Encoding;

namespace Tony.Listener.Tcp.Clients;
internal class TonyClient {
    public string Uuid { get; init; } = Guid.NewGuid().ToString();

    public int? PlayerId { get; set; }

    public required IChannel Channel { get; set; }

    public Task SendAsync( ComposerBase msg_composer ) => this.SendAsync( msg_composer.Compose() );
    public Task SendAsync( Message message ) {
        Console.WriteLine( $"SENT: {message.ToString()}" );
        return this.Channel.WriteAndFlushAsync( message );
    }
}
