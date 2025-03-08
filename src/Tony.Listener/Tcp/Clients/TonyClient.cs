using System.IO.Pipelines;
using System.Net.Sockets;
using Tony.Listener.Composers;
using Tony.Listener.Encoding;

namespace Tony.Listener.Tcp.Clients;
internal class TonyClient {
    public string Uuid { get; init; } = Guid.NewGuid().ToString();

    public int? PlayerId { get; set; }

    private readonly PipeWriter pipe;

    public async Task SendAsync( ComposerBase msg_composer ) => await this.SendAsync( msg_composer.Compose() );

    public async Task SendAsync( Message message ) {
        List<byte> buf = Base64Encoding.Encode( message.Header, 2 ).ToList();
        buf.AddRange( message.Body );
        buf.Add( 1 );

        Console.WriteLine( $"SENT: {message.ToString()}" );

        //await this.TcpClient.GetStream().WriteAsync( buf.ToArray() );
        await this.pipe.WriteAsync( buf.ToArray() );
        await this.pipe.FlushAsync();
    }

    public TonyClient( PipeWriter pipe ) {
        this.pipe = pipe;
    }
}
