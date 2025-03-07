using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp;
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

        await this.room_data.SetPlayerRoom( client.PlayerId ?? 0, room_data.Id );

        await client.SendAsync( new RoomUrlComposer() );
        await client.SendAsync( new RoomReadyComposer() { RoomId = room_data.Id, RoomModel = room_data.Model } );

        await client.SendAsync( new RoomPropertyComposer() { Property = "wallpaper", Value = 304 } );
        await client.SendAsync( new RoomPropertyComposer() { Property = "floor", Value = 108 } );

        // todo: votes
        await client.SendAsync( new UpdateVotesComposer() );

        // todo: navi events
        await client.SendAsync( new RoomEventInfoComposer() );

        //34 / @b82303 3,5,0.0,2,2 /{ 13}
        //{ 1}
        Message msg = new( 34 );
        msg.WriteDelimiter( "15000", ' ' );
        msg.WriteDelimiter( 5, ',' );
        msg.WriteDelimiter( 5, ',' );
        msg.WriteDelimiter( "1.0", ',' );
        msg.WriteDelimiter( 1, ',' );
        msg.WriteDelimiter( 1, '/' );
        msg.Write( (( char )13).ToString(), true );

        Message msg_2 = new( 361 );
        msg_2.Write( 10000 );
        msg_2.Write( false );

        await client.SendAsync( msg );
        await client.SendAsync( msg_2 );
    }
}
