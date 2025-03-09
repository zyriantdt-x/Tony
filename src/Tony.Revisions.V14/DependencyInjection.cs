using Microsoft.Extensions.DependencyInjection;
using Tony.Revisions.V14.Handlers.Handshake;
using Tony.Revisions.V14.Handlers.Messenger;
using Tony.Revisions.V14.Handlers.Navigator;
using Tony.Revisions.V14.Handlers.Player;
using Tony.Revisions.V14.Handlers.Rooms;

namespace Tony.Revisions.V14;
public static class DependencyInjection {
    public static void RegisterServices( IServiceCollection services ) {
        // handhskae
        services.AddTransient<InitCryptoHandler>();
        services.AddTransient<GenerateKeyHandler>();
        services.AddTransient<TryLoginHandler>();

        // player
        services.AddTransient<GetInfoHandler>();
        services.AddTransient<GetCreditsHandler>();

        // navi
        services.AddTransient<NavigateHandler>();

        // room
        services.AddTransient<GetRoomInfoHandler>();
        services.AddTransient<GetInterestHandler>();
        services.AddTransient<RoomDirectoryHandler>();
        services.AddTransient<TryRoomHandler>();
        services.AddTransient<GoToRoomHandler>();
        services.AddTransient<GetRoomAdHandler>();
        services.AddTransient<GetHeightmapHandler>();
        services.AddTransient<GetUsersHandler>();
        services.AddTransient<GetObjectsHandler>();
        services.AddTransient<GetItemsHandler>();

        // messenger
        services.AddTransient<MessengerInitHandler>();
    }
}
