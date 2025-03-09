﻿using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Handshake;
public class CryptoParametersComposer : ComposerBase {
    public override short Header => 277;

    public override ClientMessage Compose() {
        ClientMessage msg = new( this.Header );

        msg.Write( 0 );

        return msg;
    }
}
