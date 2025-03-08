﻿using Tony.Revisions.V14.Composers.Handshake;

using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Handshake;
[Header( 202 )]
public class GenerateKeyHandler : IHandler {
    public async Task Handle( TonyClient client, object ClientMessage ) {
        await client.SendAsync( new SessionParametersComposer() );
        await client.SendAsync( new AvailableSetsComposer() );
    }
}
