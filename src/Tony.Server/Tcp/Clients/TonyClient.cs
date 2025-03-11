using DotNetty.Transport.Channels;
using System.IO.Pipelines;
using System.Net.Sockets;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;

namespace Tony.Server.Tcp.Clients;
internal class TonyClient : ITonyClient {
    public string Uuid { get; init; } = Guid.NewGuid().ToString();

    public int PlayerId { get; set; } = -1;

    public bool HasPonged { get; set; }

    private readonly PipeWriter writer;

    public TonyClient( PipeWriter writer ) {
        this.writer = writer;
    }

    public async Task Kill() => await this.writer.CompleteAsync();

    public Task SendAsync( ComposerBase msg_composer ) => this.SendAsync( msg_composer.Compose() );
    public async Task SendAsync( ClientMessage message ) {
        Console.WriteLine( $"SENT: {message.ToString()}" );
        await this.writer.WriteAsync( message.Finalise() );
        await this.writer.FlushAsync();
    }

    public Task SendQueued( ComposerBase msg_composer ) => this.SendQueued( msg_composer.Compose() );
    public async Task SendQueued( ClientMessage message ) {
        Console.WriteLine( $"SENT: {message.ToString()}" );
        await this.writer.WriteAsync( message.Finalise() );
    }

    public async Task Flush() => await this.writer.FlushAsync();
}
