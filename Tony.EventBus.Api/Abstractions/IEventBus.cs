using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus.Api;
public interface IEventBus {
    void Subscribe( string event_name, Action<object> handler );
    Task Publish(string event_name, object message );
}
