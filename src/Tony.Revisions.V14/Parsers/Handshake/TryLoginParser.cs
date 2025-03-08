using System.Text.RegularExpressions;
using Tony.Revisions.V14.ClientMessages.Handshake;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Handshake;
[Header( 4 )]
public class TryLoginParser : IParser<TryLoginClientMessage> {
    public TryLoginClientMessage Parse( ClientMessage ClientMessage ) => new() {
        Username = Regex.Replace( ClientMessage.ReadString(), @"[^a-zA-Z0-9\-]", "" ),
        Password = Regex.Replace( ClientMessage.ReadString(), @"[^a-zA-Z0-9\-]", "" )
    };
}
