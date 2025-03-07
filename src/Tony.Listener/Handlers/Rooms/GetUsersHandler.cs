﻿using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Listener.Handlers.Rooms;
[Header( 61 )]
class GetUsersHandler : IHandler {
    private readonly RoomEntityService entity_service;
    private readonly RoomDataService room_data;

    public GetUsersHandler( RoomEntityService entity_service, RoomDataService room_data ) {
        this.entity_service = entity_service;
        this.room_data = room_data;
    }

    public async Task Handle( TonyClient client, object message ) {
        RoomDataDto? room = await this.room_data.GetPlayerRoomData(client.PlayerId ?? 0);
        if( room is null )
            return;

        await client.SendAsync( new UserObjectsComposer() {
            Entities = await this.entity_service.GetEntitiesInRoom( room.Id )
        } );
    }
}
