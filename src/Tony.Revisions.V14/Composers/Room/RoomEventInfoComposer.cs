﻿using Tony.Revisions.Tcp;

namespace Tony.Revisions.Composers.Room;
internal class RoomEventInfoComposer : ComposerBase {
    public override short Header => 370;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( (-1).ToString() );
        return msg;
    }
}
