using Tony.Revisions.V14.Composers.Room;
using Tony.Revisions.V14.Messages.Rooms;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.PubSub;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 59 )]
public class GoToRoomHandler : IHandler<GoToRoomMessage> {
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

    public async Task Handle( ITonyClient client, GoToRoomMessage message ) {
        RoomDataDto? room_data = await this.room_data.GetRoomDataById( message.RoomId );
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
            PosZ = room_data.Model.DoorZ,

            HeadRotation = room_data.Model.DoorDir,
            BodyRotation = room_data.Model.DoorDir,

            RoomId = room_data.Id
        };

        await this.player_data.SetPlayerRoom( player.Id, new() {
            InstanceId = entity.InstanceId,
            RoomId = room_data.Id
        } );

        await this.entity_service.AddEntityToRoom( entity );

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
