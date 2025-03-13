using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.PubSub;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;

namespace Tony.Revisions.V14.Composers.Handshake;
[Header( 137 )]
internal class DisconnectHandler : IHandler {
    private readonly IPlayerService player_service;
    private readonly IRoomEntityService entity_service;
    private readonly IPublisherService publisher;

    public DisconnectHandler( IPlayerService player_service, IRoomEntityService entity_service, IPublisherService publisher ) {
        this.player_service = player_service;
        this.entity_service = entity_service;
        this.publisher = publisher;
    }

    public async Task Handle( ITonyClient client, object message ) {
        PlayerRoomDto? room_data = await this.player_service.GetPlayerRoom( client.PlayerId );
        if( room_data is not null ) {
            await this.entity_service.RemoveEntityFromRoom( new() { RoomId = room_data.RoomId, InstanceId = room_data.InstanceId } ); // idk if i like this
        }

        // plan for other things here...
    }
}
