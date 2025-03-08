using Tony.Revisions.V14.ClientMessages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 21 )]
public class GetRoomInfoHandler : IHandler<GetRoomInfoClientMessage> {
    private readonly RoomDataService room_data;
    private readonly PlayerDataService player_data;

    public GetRoomInfoHandler( RoomDataService room_data, PlayerDataService player_data ) {
        this.room_data = room_data;
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, GetRoomInfoClientMessage ClientMessage ) {
        RoomDataDto? room = await this.room_data.GetRoomDataById( ClientMessage.RoomId );
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
