using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tony.Listener.Handlers;
using Tony.Listener.Parsers;
using Tony.Listener.Tcp.Clients;

namespace Tony.Listener.Tcp;
internal class TonyChannelHandler : ChannelHandlerAdapter {
    public static AttributeKey<TonyClient> SESSION_ATTRIBUTE = AttributeKey<TonyClient>.NewInstance( "SESSION" );

    private readonly ILogger<TonyChannelHandler> logger;
    private readonly TonyClientService session_service;

    private readonly ParserRegistry parsers;
    private readonly HandlerRegistry handlers;

    public override bool IsSharable => true;

    public TonyChannelHandler( ILogger<TonyChannelHandler> logger,
                           TonyClientService session_service,
                           HandlerRegistry handlers,
                           ParserRegistry parsers) {
        this.logger = logger;
        this.session_service = session_service;

        this.handlers = handlers;
        this.parsers = parsers;
    }

    public override void ChannelActive( IChannelHandlerContext ctx ) {
        base.ChannelActive( ctx );

        TonyClient session = new() { Channel = ctx.Channel };
        ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).SetIfAbsent( session );
        this.session_service.RegisterClient( session );

        this.logger.LogInformation( $"New connection from {ctx.Channel.RemoteAddress}" );

        session.Send( new HelloPacketComposer() );
    }

    public override void ChannelInactive( IChannelHandlerContext ctx ) {
        base.ChannelInactive( ctx );

        TonyClient? session = ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).Get();
        if( session == null ) {
            this.logger.LogError( $"Session-less channel closed ({ctx.Channel.RemoteAddress})" );
            return;
        }

        if( session.PlayerId != null ) {
            // do some stuff
        }

        this.session_service.DeregisterClient( session );

        this.logger.LogInformation( $"Connection from {ctx.Channel.RemoteAddress} closed" );
    }

    public override void ChannelRead( IChannelHandlerContext ctx, object msg ) {
        TonyClient? session = ctx.Channel.GetAttribute( SESSION_ATTRIBUTE ).Get();
        if( session == null ) {
            this.logger.LogError( $"Message read from session-less channel ({ctx.Channel.RemoteAddress})" );
            return;
        }

        if( msg is not Message ) {
            this.logger.LogError( $"Message read was not parsed ({ctx.Channel.RemoteAddress})" );
            return;
        }

        Message request = ( Message )msg;

        _ = Task.Run(() => this.ProcessMessage( session, request ) );

        base.ChannelRead( ctx, msg );
    }

    public override void ChannelReadComplete( IChannelHandlerContext context )
        => context.Flush();

    private async Task ProcessMessage( TonyClient client, Message message ) {
        this.logger.LogInformation( $"[{client.Uuid}][i]: {message.ToString()}" );

        // this should probably be an exception - might change that in prod
        IParser? parser = this.parsers.GetParser( message.Header );
        if( parser is null ) {
            this.logger.LogWarning( $"Failed to find parser for header {message.Header}" );
            return;
        }

        Handlers.IHandler? handler = this.handlers.GetHandler( message.Header );
        if( handler is null ) {
            this.logger.LogWarning( $"Failed to find handler for header {message.Header}" );
            return;
        }

        object parsed_message = parser.Parse( message );

        await handler.Handle( client, parsed_message );
        this.logger.LogInformation( $"Handler {handler.GetType().Name} completed." );
    }
}
