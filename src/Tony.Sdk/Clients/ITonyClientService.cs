using Tony.Sdk.Revisions;

namespace Tony.Sdk.Clients;
public interface ITonyClientService {
    void RegisterClient( ITonyClient client );
    void DeregisterClient( ITonyClient client );
    ITonyClient? GetClient( string uuid );
    ITonyClient? GetClient( int player_id );
    Task SendToAll( ComposerBase msg_composer );
    Task SendToMany( IEnumerable<int> player_ids, ComposerBase msg_composer );
}
