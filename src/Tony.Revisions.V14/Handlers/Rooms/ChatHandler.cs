using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.PubSub.Events.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Sdk.Revisions;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 52 )]
public class ChatHandler : IHandler<ChatMessage> {
    private readonly IRoomEntityService entity_service;
    private readonly IPlayerService player_service;
    private readonly IPublisherService publisher;

    public ChatHandler( IRoomEntityService entity_service, IPlayerService player_service, IPublisherService publisher ) {
        this.entity_service = entity_service;
        this.player_service = player_service;
        this.publisher = publisher;
    }

    public async Task Handle( ITonyClient client, ChatMessage message ) {
        PlayerRoomDto? player_room = await this.player_service.GetPlayerRoom( client.PlayerId );
        if( player_room is null )
            return;

        IEnumerable<RoomEntityDto> audience = (await this.entity_service.GetEntitiesInRoom( player_room.RoomId )).Where( e => e.EntityType == EntityType.PLAYER );
        if( !audience.Any() )
            return;

        await this.publisher.Publish( new RoomChatEvent() {
            Audience = audience.Select( e => e.EntityId ),
            SenderInstanceId = player_room.InstanceId,
            Type = ChatType.SPEAK,
            Message = message.Message
        } );
    }
}
