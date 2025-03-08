using System.Text.RegularExpressions;
using Tony.Revisions.V14.Messages.Handshake;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Handshake;
[Header( 4 )]
public class TryLoginParser : IParser<TryLoginClientMessage> {
    public TryLoginClientMessage Parse( ClientMessage msg ) => new() {
        Username = Regex.Replace( msg.ReadString(), @"[^a-zA-Z0-9\-]", "" ),
        Password = Regex.Replace( msg.ReadString(), @"[^a-zA-Z0-9\-]", "" )
    };
}
