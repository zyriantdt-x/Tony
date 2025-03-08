using System.Text.RegularExpressions;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 52 )]
internal class ChatParser : IParser<ChatMessage> {
    public ChatMessage Parse( Message message ) => new() {
        Message = Regex.Replace( message.ReadString(), @"[^a-zA-Z0-9\-_@!]", "" ) // maybe we change this?
    };
}
