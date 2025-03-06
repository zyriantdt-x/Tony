using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Player;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Listener.Handlers.Rooms;
[Header( 21 )]
internal class GetRoomInfoHandler : IHandler<GetRoomInfoMessage> {
    private readonly RoomDataService room_data;
    private readonly PlayerDataService player_data;

    public GetRoomInfoHandler( RoomDataService room_data, PlayerDataService player_data ) {
        this.room_data = room_data;
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, GetRoomInfoMessage message ) {
        RoomDataDto? room = await this.room_data.GetRoomDataById( message.RoomId );
        if( room is null )
            return;

        string? owner_name = await this.player_data.GetUsernameById( room.OwnerId );
        if( owner_name is null )
            return;

        await client.SendAsync( new RoomInfoComposer() {
            RoomData = room,
            OwnerUsername = owner_name
        } );
    }
}
