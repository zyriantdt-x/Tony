using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 52 )]
public class ChatHandler : IHandler<ChatMessage> {
    private readonly IRoomEntityService entity_service;
    private readonly IPlayerService player_service;

    public ChatHandler( IRoomEntityService entity_service, IPlayerService player_service ) {
        this.entity_service = entity_service;
        this.player_service = player_service;
    }

    public async Task Handle( ITonyClient client, ChatMessage message ) {
        PlayerRoomDto? player_room = await this.player_service.GetPlayerRoom( client.PlayerId );
        if( player_room is null )
            return;

        RoomEntityDto? player_entity = await this.entity_service.GetPlayerEntity( player_room );
        if( player_entity is null )
            return;

        await this.entity_service.EntityChat( player_entity, ChatType.SPEAK, message.Message );
    }
}
