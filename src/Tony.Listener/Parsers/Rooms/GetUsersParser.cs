﻿using Tony.Listener.Messages.Rooms;
using Tony.Listener.Tcp;

namespace Tony.Listener.Parsers.Rooms;
class GetUsersParser : IParser<GetUsersMessage> {
    public GetUsersMessage Parse( Message message ) => new();
}
