using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Room;
internal class HeightmapComposer : ComposerBase {
    public override short Header => 31;

    public string Heightmap { get; set; } = String.Empty;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( this.Heightmap, true );
        return msg;
    }
}
