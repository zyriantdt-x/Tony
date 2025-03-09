using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 60 )]
public class GetHeightmapHandler : IHandler {
    private readonly IRoomDataService room_data;

    public GetHeightmapHandler( IRoomDataService room_data ) {
        this.room_data = room_data;
    }

    public async Task Handle( ITonyClient client, object message ) {
        /*RoomDataDto? player_room = await this.room_data.GetPlayerRoomData( client.PlayerId ?? 0 );
        if( player_room is null )
            return;

        RoomModelDto? model = await this.room_data.GetRoomModelById( player_room.Model );
        if( model is null )
            return;

        await client.SendAsync( new HeightmapComposer() { Heightmap = model.ParseHeightmap() } );*/
    }
}
