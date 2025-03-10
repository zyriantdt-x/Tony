﻿using Tony.Revisions.V14.PubSub.Events.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Revisions.PubSub.ServerEvents;
using Tony.Sdk.Services;

namespace Tony.Revisions.V14.PubSub.Handlers.ServerEvents;
[Event( "channel_disconnected" )]
internal class ChannelDisconnectedHandler : IPubSubHandler<ChannelDisconnectedEvent> {
    private readonly ITonyClientService client_service;
    private readonly IPlayerService player_service;
    private readonly IRoomEntityService entity_service;
    private readonly IPublisherService publisher;

    public ChannelDisconnectedHandler( ITonyClientService client_service, 
                                       IPlayerService player_service, 
                                       IRoomEntityService entity_service,
                                       IPublisherService publisher ) {
        this.client_service = client_service;
        this.player_service = player_service;
        this.entity_service = entity_service;
        this.publisher = publisher;
    }

    public async Task Handle( ChannelDisconnectedEvent message ) {
        ITonyClient? client = this.client_service.GetClient( message.ClientId );
        if( client is null )
            return;

        PlayerRoomDto? player_room = await this.player_service.GetPlayerRoom( client.PlayerId );
        if( player_room is not null ) {
            await this.player_service.SetPlayerRoom( client.PlayerId ); // remove from player map
            await this.entity_service.RemoveEntityFromRoom( player_room.RoomId, player_room.InstanceId );

            // send updated users
            IEnumerable<RoomEntityDto> entities = await this.entity_service.GetEntitiesInRoom( player_room.RoomId );
            await this.publisher.Publish( new RoomEntitiesUpdatedEvent() {
                Audience = entities.Where( e => e.EntityType == EntityType.PLAYER ).Select( e => e.EntityId ),
                Entities = entities
            } );
        }
    }
}
