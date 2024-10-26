using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Server.Clients;
internal class MessageSender : IMessageSender {
    private readonly ILogger<MessageSender> logger;

    public MessageSender( ILogger<MessageSender> logger ) {
        this.logger = logger;
    }

    public async Task SendAsync( ITonyClient recipient, string message ) {
        await Task.Run( () => recipient.FleckConnection.Send( message ) );

        this.logger.LogInformation( $"Sent socket message -> {recipient.Uuid}@{recipient.FleckConnection.ConnectionInfo.ClientIpAddress}" );
    }
}
