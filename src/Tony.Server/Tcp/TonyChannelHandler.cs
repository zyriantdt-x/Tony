using DotNetty.Common.Utilities;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using Tony.Sdk.Clients;
using Tony.Sdk.Revisions;
using Tony.Sdk.Revisions.PubSub;
using Tony.Sdk.Revisions.PubSub.ServerEvents;
using Tony.Server.Tcp.Clients;
using Tony.Server.Tcp.Registries;

namespace Tony.Server.Tcp;
internal class TonyChannelHandler : ChannelHandlerAdapter {
    public static AttributeKey<TonyClient> SESSION_ATTRIBUTE = AttributeKey<TonyClient>.NewInstance( "SESSION" );

    private readonly ILogger<TonyChannelHandler> logger;
    private readonly ITonyClientService session_service;

    private readonly ParserRegistry parsers;
    private readonly HandlerRegistry handlers;

    private readonly IPublisherService publisher;

    public override bool IsSharable => true;

    public TonyChannelHandler( ILogger<TonyChannelHandler> logger,
                               ITonyClientService session_service,
                               HandlerRegistry handlers,
                               ParserRegistry parsers,
                               IPublisherService publisher ) {
        this.logger = logger;
        this.session_service = session_service;

        this.handlers = handlers;
        this.parsers = parsers;

        this.publisher = publisher;
    }

    public override void ChannelActive( IChannelHandlerContext ctx ) {
        base.ChannelActive( ctx );

        TonyClient session = new() { Channel = ctx.Channel };
        ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).SetIfAbsent( session );
        this.session_service.RegisterClient( session );

        this.logger.LogInformation( $"New connection from {ctx.Channel.RemoteAddress}" );

        _ = Task.Run( () => this.publisher.Publish( new ChannelConnectedEvent() {
            ClientId = session.Uuid
        } ) );
    }

    public override void ChannelInactive( IChannelHandlerContext ctx ) {
        base.ChannelInactive( ctx );

        TonyClient? session = ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).Get();
        if( session == null ) {
            this.logger.LogError( $"Session-less channel closed ({ctx.Channel.RemoteAddress})" );
            return;
        }

        _ = Task.Run( () => this.publisher.Publish( new ChannelDisconnectedEvent() {
            ClientId = session.Uuid
        } ) );

        this.session_service.DeregisterClient( session );

        this.logger.LogInformation( $"Connection from {ctx.Channel.RemoteAddress} closed" );
    }

    public override void ChannelRead( IChannelHandlerContext ctx, object msg ) {
        TonyClient? session = ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).Get();
        if( session == null ) {
            this.logger.LogError( $"Message read from session-less channel ({ctx.Channel.RemoteAddress})" );
            return;
        }

        if( msg is not ClientMessage ) {
            this.logger.LogError( $"Message read was not parsed ({ctx.Channel.RemoteAddress})" );
            return;
        }

        ClientMessage request = ( ClientMessage )msg;

        _ = Task.Run( () => this.ProcessMessage( session, request ) );

        base.ChannelRead( ctx, msg );
    }

    public override void ChannelReadComplete( IChannelHandlerContext context )
        => context.Flush();

    private async Task ProcessMessage( TonyClient client, ClientMessage message ) {
        this.logger.LogInformation( $"[{client.Channel.RemoteAddress}][i]: {message.ToString()}" );

        // this should probably be an exception - might change that in prod
        IHandler? handler = this.handlers.GetHandler( message.Header );
        if( handler is null ) {
            this.logger.LogWarning( $"Failed to find handler for header {message.Header}" );
            return;
        }

        IParser? parser = this.parsers.GetParser( message.Header );
        if( parser is null ) {
            await handler.Handle( client, message );
            return;
        }

        object parsed_message = parser.Parse( message );

        await handler.Handle( client, parsed_message );
    }

    public override void ExceptionCaught( IChannelHandlerContext context, Exception exception ) {
        this.logger.LogError( $"Dotnetty caught an exception: {exception}" );
        context.CloseAsync();
    }

    public override void UserEventTriggered( IChannelHandlerContext context, object evt ) {
        if( evt is IdleStateEvent idle_event && idle_event.State == IdleState.ReaderIdle ) {
            this.logger.LogInformation( "Client timed out: " + context.Channel.RemoteAddress );
            context.CloseAsync();
        }
    }
}
