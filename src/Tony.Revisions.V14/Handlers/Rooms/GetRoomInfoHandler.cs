using Tony.Revisions.V14.Messages.Rooms;
using Tony.Revisions.V14.Composers.Room;
using Tony.Sdk.Revisions;
using Tony.Sdk.Services;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
namespace Tony.Revisions.V14.Handlers.Rooms;
[Header( 21 )]
public class GetRoomInfoHandler : IHandler<GetRoomInfoMessage> {
    private readonly IRoomDataService room_data;

    public GetRoomInfoHandler( IRoomDataService room_data ) {
        this.room_data = room_data;
    }

    public async Task Handle( ITonyClient client, GetRoomInfoMessage message ) {
        RoomDataDto? room = await this.room_data.GetRoomDataById( message.RoomId );
        if( room is null )
            return;

        await client.SendAsync( new RoomInfoComposer() {
            RoomData = room
        } );
    }
}
