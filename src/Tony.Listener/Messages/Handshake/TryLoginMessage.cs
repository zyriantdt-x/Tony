﻿namespace Tony.Listener.Messages.Handshake;
internal class TryLoginMessage {
    public required string Username { get; init; }
    public required string Password { get; init; }
}
