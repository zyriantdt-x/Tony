﻿using DotNetty.Transport.Channels;
using System.Net.Sockets;
using Tony.Sdk.Clients;
using Tony.Sdk.Composers;

namespace Tony.Server.Tcp.Clients;
internal class TonyClient : ITonyClient {
    public string Uuid { get; init; } = Guid.NewGuid().ToString();

    public int? PlayerId { get; set; }

    public required IChannel Channel { get; set; }

    public Task SendAsync( ComposerBase msg_composer ) => this.SendAsync( msg_composer.Compose() );
    public Task SendAsync( Message message ) {
        Console.WriteLine( $"SENT: {message.ToString()}" );
        return this.Channel.WriteAndFlushAsync( message );
    }

    public Task SendQueued( ComposerBase msg_composer ) => this.SendQueued( msg_composer.Compose() );
    public Task SendQueued( Message message ) {
        Console.WriteLine( $"SENT: {message.ToString()}" );
        return this.Channel.WriteAsync( message );
    }

    public void Flush() => this.Channel.Flush();
}
