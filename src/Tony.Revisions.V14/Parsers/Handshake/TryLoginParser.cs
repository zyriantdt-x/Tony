using System.Text.RegularExpressions;
using Tony.Listener.Messages.Handshake;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Handshake;
[Header( 4 )]
internal class TryLoginParser : IParser<TryLoginMessage> {
    public TryLoginMessage Parse( Message message ) => new() {
        Username = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-]", "" ),
        Password = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-]", "" )
    };
}
