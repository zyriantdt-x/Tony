using Tony.Revisions.V14.Composers.Handshake;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Handlers.Handshake;
[Header(206)]
public class InitCryptoHandler : IHandler
{
    public async Task Handle(TonyClient client, object message) => await client.SendAsync(new CryptoParametersComposer());
}
