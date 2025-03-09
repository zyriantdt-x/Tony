using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
using Tony.Sdk.Clients;
using Tony.Sdk.Services;
using Tony.Sdk.Dto;
using System.Reflection;
using Tony.Sdk.Revisions.PubSub;
using Tony.Revisions.V14.PubSub.Events.Rooms;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 59 )]
public class GoToRoomHandler : IHandler<GoToRoomClientMessage> {
    private readonly IRoomDataService room_data;
    private readonly IRoomEntityService entity_service;
    private readonly IPlayerService player_data;
    private readonly IPublisherService publisher;

    public GoToRoomHandler( IRoomDataService room_data,
                            IRoomEntityService entity_service,
                            IPlayerService player_data,
                            IPublisherService publisher ) {
        this.room_data = room_data;
        this.entity_service = entity_service;
        this.player_data = player_data;
        this.publisher = publisher;
    }

    public async Task Handle( ITonyClient client, GoToRoomClientMessage ClientMessage ) {
        RoomDataDto? room_data = await this.room_data.GetRoomDataById( ClientMessage.RoomId );
        if( room_data is null )
            return;

        PlayerDto? player = await this.player_data.GetPlayerById( client.PlayerId );
        if( player is null )
            return;

        RoomEntityDto entity = new() {
            InstanceId = new Random().Next( 10000, 100000 ),
            EntityId = player.Id,
            EntityType = EntityType.PLAYER,
            Username = player.Username,
            Figure = player.Figure,
            Sex = player.Sex ? "M" : "F",
            Motto = player.Mission,

            PosX = room_data.Model.DoorX,
            PosY = room_data.Model.DoorY,
            PosZ = room_data.Model.DoorZ
        };

        await this.player_data.SetPlayerRoom( player.Id, new() {
            InstanceId = entity.InstanceId,
            RoomId = room_data.Id
        } );

        await this.entity_service.AddEntityToRoom( room_data.Id, entity );
        IEnumerable<RoomEntityDto> updated_entities = await this.entity_service.GetEntitiesInRoom( room_data.Id );

        await this.publisher.Publish( new RoomEntitiesUpdatedEvent() {
            Audience = updated_entities.Where( entity => entity.EntityType == EntityType.PLAYER ).Select( entity => entity.EntityId ),
            Entities = updated_entities
        } );

        await client.SendAsync( new RoomUrlComposer() );
        await client.SendAsync( new RoomReadyComposer() { RoomId = room_data.Id, RoomModel = room_data.ModelId } );

        await client.SendAsync( new RoomPropertyComposer() { Property = "wallpaper", Value = room_data.Wallpaper } );
        await client.SendAsync( new RoomPropertyComposer() { Property = "floor", Value = room_data.Floor } );

        // todo: votes
        await client.SendAsync( new UpdateVotesComposer() );

        // todo: navi events
        await client.SendAsync( new RoomEventInfoComposer() );
    }
}
