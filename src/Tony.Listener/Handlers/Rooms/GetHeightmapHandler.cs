using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Listener.Handlers.Rooms;
[Header( 60 )]
internal class GetHeightmapHandler : IHandler {
    private readonly RoomDataService room_data;

    public GetHeightmapHandler( RoomDataService room_data ) {
        this.room_data = room_data;
    }

    public async Task Handle( TonyClient client, object message ) {
        RoomDataDto? player_room = await this.room_data.GetPlayerRoomData( client.PlayerId ?? 0 );
        if( player_room is null )
            return;

        RoomModelDto? model = await this.room_data.GetRoomModelById( player_room.Model );
        if( model is null )
            return;

        await client.SendAsync( new HeightmapComposer() { Heightmap = model.ParseHeightmap() } );
    }
}
