﻿using Tony.Revisions.V14.ClientMessages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 59 )]
public class GoToRoomHandler : IHandler<GoToRoomClientMessage> {
    private readonly RoomDataService room_data;
    private readonly RoomEntityService entity_service;
    private readonly PlayerDataService player_data;

    public GoToRoomHandler( RoomDataService room_data, RoomEntityService entity_service, PlayerDataService player_data ) {
        this.room_data = room_data;
        this.entity_service = entity_service;
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, GoToRoomClientMessage ClientMessage ) {
        RoomDataDto? room_data = await this.room_data.GetRoomDataById( ClientMessage.RoomId );
        if( room_data is null )
            return;

        PlayerDto? player = await this.player_data.GetUserObject( client.PlayerId ?? 0 );
        if( player is null )
            return;

        RoomModelDto? model = await this.room_data.GetRoomModelById( room_data.Model );
        if( model is null )
            return;

        await this.room_data.SetPlayerRoom( client.PlayerId ?? 0, room_data.Id );
        await this.entity_service.AddEntityToRoom( room_data.Id, new() {
            InstanceId = new Random().Next( 10000, 100000 ),
            EntityId = player.Id,
            EntityType = EntityType.PLAYER,
            Username = player.Username,
            Figure = player.Figure,
            Sex = player.Sex ? "M" : "F",
            Motto = player.Mission,

            PosX = model.DoorX,
            PosY = model.DoorY,
            PosZ = model.DoorZ
        } );

        await client.SendAsync( new RoomUrlComposer() );
        await client.SendAsync( new RoomReadyComposer() { RoomId = room_data.Id, RoomModel = room_data.Model } );

        await client.SendAsync( new RoomPropertyComposer() { Property = "wallpaper", Value = 304 } );
        await client.SendAsync( new RoomPropertyComposer() { Property = "floor", Value = 108 } );

        // todo: votes
        await client.SendAsync( new UpdateVotesComposer() );

        // todo: navi events
        await client.SendAsync( new RoomEventInfoComposer() );
    }
}
