using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus;
internal interface IEventBusEnvironment {
    Task Run();
}
