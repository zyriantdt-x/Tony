﻿using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Tony.Listener.Options;
using Tony.Protos;

namespace Tony.Listener.Services.Player;
internal class PlayerDataService {
    private readonly PlayerDataEndpoint.PlayerDataEndpointClient client;

    public PlayerDataService( IOptions<PlayerOptions> options ) {
        this.client = new( GrpcChannel.ForAddress( options.Value.PlayerServiceAddress ) );
    }

    public async Task<UserObjectResponse> GetUserObject( int id ) {
        UserObjectResponse res = await this.client.GetUserObjectAsync( new() {
            Id = id
        }, new() );

        return res;
    }
}
