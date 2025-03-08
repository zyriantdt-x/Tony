using Tony.Revisions.V14.Composers.Messenger;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Handlers.Messenger;
[Header(12)]
public class MessengerInitHandler : IHandler
{
    public async Task Handle(TonyClient client, object message) => await client.SendAsync(new MessengerInitComposer());
}
