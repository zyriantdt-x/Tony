﻿using Tony.Revisions.V14.Composers.Room;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 61 )]
class GetUsersHandler : IHandler {
    private readonly RoomEntityService entity_service;
    private readonly RoomDataService room_data;

    public GetUsersHandler( RoomEntityService entity_service, RoomDataService room_data ) {
        this.entity_service = entity_service;
        this.room_data = room_data;
    }

    public async Task Handle( TonyClient client, object message ) {
        RoomDataDto? room = await this.room_data.GetPlayerRoomData( client.PlayerId ?? 0 );
        if( room is null )
            return;

        await client.SendAsync( new UserObjectsComposer() {
            Entities = await this.entity_service.GetEntitiesInRoom( room.Id )
        } );
    }
}
