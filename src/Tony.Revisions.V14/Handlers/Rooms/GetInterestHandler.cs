using Tony.Revisions.Composers.Room;
using Tony.Revisions.Messages.Rooms;
using Tony.Revisions.Parsers;
using Tony.Revisions.Tcp.Clients;

namespace Tony.Revisions.Handlers.Rooms;
[Header( 182 )]
internal class GetInterestHandler : IHandler {
    public async Task Handle( TonyClient client, object message ) => await client.SendAsync( new RoomInterestComposer() );
}
