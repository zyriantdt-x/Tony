using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus;
internal interface IEventBusPublisher {
    void Publish( string message );
}
