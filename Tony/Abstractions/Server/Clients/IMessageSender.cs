using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal interface IMessageSender {
    Task SendAsync( ITonyClient recipient, string message );
}
