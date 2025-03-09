using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 52 )]
public class ChatHandler : IHandler<ChatClientMessage> {
    //private readonly RoomEntityService entity_service;
    private readonly IRoomDataService room_service;

    public ChatHandler( /*RoomEntityService entity_service,*/ IRoomDataService room_service ) {
        //this.entity_service = entity_service;
        this.room_service = room_service;
    }

    public async Task Handle( ITonyClient client, ChatClientMessage message ) { }
        // do we make the room service orchestrate sending to other servers or do we do it here?



        //await this.entity_service.EntityChat()

}
