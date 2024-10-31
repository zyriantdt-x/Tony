using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.EventBus;
internal class EventBusPublisher : IEventBusPublisher {
    private readonly ILogger<EventBusPublisher> logger;

    private IConnection connection;

    public EventBusPublisher( ILogger<EventBusPublisher> logger ) {
        this.logger = logger;

        this.connection = this.CreateConnection();
    }

    private IConnection CreateConnection() {
        string hostname = Environment.GetEnvironmentVariable( "tony.rabbitmq.hostname" ) ?? throw new Exception( "No RabbitMQ hostname" );
        string username = Environment.GetEnvironmentVariable( "tony.rabbitmq.username" ) ?? throw new Exception( "No RabbitMQ Username" );
        string password = Environment.GetEnvironmentVariable( "tony.rabbitmq.password" ) ?? throw new Exception( "No RabbitMQ Password" );

        ConnectionFactory connection_factory = new() {
            HostName = hostname,
            UserName = username,
            Password = password
        };

        return connection_factory.CreateConnection();
    }

    public void Publish( string message ) {
        if( this.connection == null || !this.connection.IsOpen ) {
            this.CreateConnection();
        }

        using IModel channel = this.connection!.CreateModel();
        channel.QueueDeclare( queue: "tony-bus-1",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null );

        byte[] body = Encoding.UTF8.GetBytes( message );

        channel.BasicPublish( exchange: "",
                              routingKey: "tony-bus-1",
                              basicProperties: null,
                              body: body );

        this.logger.LogInformation( $"RabbitMQ message published -> {message}" );
    }
}
