using Tony.Listener.Composers.Room;
using Tony.Listener.Messages.Rooms;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Handlers.Rooms;
[Header( 182 )]
internal class GetInterestHandler : IHandler<GetInterestMessage> {
    public async Task Handle( TonyClient client, GetInterestMessage message ) => await client.SendAsync( new RoomInterestComposer() );
}
