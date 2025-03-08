using Tony.Revisions.V14.Composers.Room;
using Tony.Revisions.V14.Messages.Rooms;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Handlers.Rooms;
[Header(2)]
public class RoomDirectoryHandler : IHandler<RoomDirectoryMessage>
{
    public async Task Handle(TonyClient client, RoomDirectoryMessage message)
    {
        if (!message.IsPublic)
        {
            await client.SendAsync(new OpenConnectionComposer());
            return;
        }

        // handle public room
    }
}
