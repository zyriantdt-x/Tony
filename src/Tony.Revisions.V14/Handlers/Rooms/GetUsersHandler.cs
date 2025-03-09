using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 61 )]
class GetUsersHandler : IHandler {
    private readonly IRoomEntityService entity_service;
    private readonly IRoomDataService room_data;
    private readonly IPlayerService player_data;

    public GetUsersHandler( IRoomEntityService entity_service, IRoomDataService room_data, IPlayerService player_data ) {
        this.entity_service = entity_service;
        this.room_data = room_data;
        this.player_data = player_data;
    }

    public async Task Handle( ITonyClient client, object ClientMessage ) {
        PlayerRoomDto? player_room = await this.player_data.GetPlayerRoom( client.PlayerId );
        if( player_room is null )
            return;

        RoomDataDto? room = await this.room_data.GetRoomDataById( player_room.RoomId );
        if( room is null )
            return;

        await client.SendAsync( new UserObjectsComposer() {
            Entities = await this.entity_service.GetEntitiesInRoom( room.Id )
        } );
    }
}
