using System.Reflection;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 60 )]
public class GetHeightmapHandler : IHandler {
    private readonly IRoomDataService room_data;
    private readonly IPlayerService player_data;

    public GetHeightmapHandler( IPlayerService player_data, IRoomDataService room_data ) {
        this.room_data = room_data;
        this.player_data = player_data;
    }

    public async Task Handle( ITonyClient client, object message ) {
        PlayerRoomDto? player_room = await this.player_data.GetPlayerRoom( client.PlayerId );
        if( player_room is null )
            return;

        RoomDataDto? room = await this.room_data.GetRoomDataById( player_room.RoomId );
        if( room is null )
            return;

        await client.SendAsync( new HeightmapComposer() { Heightmap = room.Model.ParseHeightmap() } );
    }
}
