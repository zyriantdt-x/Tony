using System.Text.RegularExpressions;
using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Parsers.Rooms;
[Header( 52 )]
public class ChatParser : IParser<ChatMessage> {
    public ChatMessage Parse( ClientMessage message ) => new() {
        Message = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-_@!]", "" ) // maybe we change this?
    };
}
