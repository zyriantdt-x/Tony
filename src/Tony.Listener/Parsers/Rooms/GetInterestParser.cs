using Tony.Listener.Messages.Player;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 182 )]
internal class GetInterestParser : IParser<GetInterestMessage> {
    public GetInterestMessage Parse( Message message )
        => new(); // apparently we don't care
}
