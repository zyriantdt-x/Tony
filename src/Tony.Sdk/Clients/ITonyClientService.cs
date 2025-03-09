using Tony.Sdk.Revisions;

namespace Tony.Sdk.Clients;
public interface ITonyClientService {
    void RegisterClient( ITonyClient client );
    void DeregisterClient( ITonyClient client );
    ITonyClient? GetClient( string uuid );
    Task SendToAll( ComposerBase msg_composer );
    Task SendToMany( List<int> player_ids, ComposerBase msg_composer );
}
