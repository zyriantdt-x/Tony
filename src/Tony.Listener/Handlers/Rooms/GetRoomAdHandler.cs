using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 126 )]
internal class GetRoomAdHandler : IHandler<GetRoomAdMessage> {
    public async Task Handle( TonyClient client, GetRoomAdMessage message ) => await client.SendAsync( new RoomAdComposer() );
}
