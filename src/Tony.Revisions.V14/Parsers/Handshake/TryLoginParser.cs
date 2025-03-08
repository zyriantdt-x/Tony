using System.Text.RegularExpressions;
using Tony.Revisions.V14.Messages.Handshake;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Handshake;
[Header( 4 )]
public class TryLoginParser : IParser<TryLoginMessage> {
    public TryLoginMessage Parse( Message message ) => new() {
        Username = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-]", "" ),
        Password = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-]", "" )
    };
}
