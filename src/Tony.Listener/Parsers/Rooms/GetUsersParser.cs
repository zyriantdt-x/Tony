using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
[Header( 63 )]
class GetUsersParser : IParser<GetUsersMessage> {
    public GetUsersMessage Parse( Message message ) => new();
}
