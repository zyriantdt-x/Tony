using Tony.Revisions.V14.Composers.Room;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Handlers.Rooms;
[Header(62)]
public class GetObjectsHandler : IHandler
{
    public async Task Handle(TonyClient client, object message)
    {
        await client.SendQueued(new ObjectsWorldComposer());
        await client.SendQueued(new ActiveObjectsComposer());

        client.Flush();
    }
}
