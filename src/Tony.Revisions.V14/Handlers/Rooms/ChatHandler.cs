using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Parsers;
using Tony.Revisions.Services.Rooms;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 52 )]
public class ChatHandler : IHandler<ChatMessage> {
    private readonly RoomEntityService entity_service;
    private readonly RoomDataService room_service;

    public ChatHandler( RoomEntityService entity_service, RoomDataService ) {
        this.entity_service = entity_service;
        this.room_service = room_service;
    }

    public async Task Handle( TonyClient client, ChatMessage message ) {
        // do we make the room service orchestrate sending to other servers or do we do it here?

        

        await this.entity_service.EntityChat()
    }
}
