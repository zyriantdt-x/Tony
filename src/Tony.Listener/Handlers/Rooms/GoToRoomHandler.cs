using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Listener.Handlers.Rooms;
[Header( 59 )]
internal class GoToRoomHandler : IHandler<GoToRoomMessage> {
    private readonly RoomDataService room_data;

    public GoToRoomHandler( RoomDataService room_data ) {
        this.room_data = room_data;
    }

    public async Task Handle( TonyClient client, GoToRoomMessage message ) {
        RoomDataDto? room_data = await this.room_data.GetRoomDataById( message.RoomId );
        if( room_data is null )
            return;

        await client.SendAsync( new RoomUrlComposer() );
        await client.SendAsync( new RoomReadyComposer() { RoomId = room_data.Id, RoomModel = room_data.Model } );

        await client.SendAsync( new RoomPropertyComposer() { Property = "wallpaper", Value = room_data.Wallpaper } );
        await client.SendAsync( new RoomPropertyComposer() { Property = "floor", Value = room_data.Floor } );

        // todo: votes
        // todo: navi events
    }
}
