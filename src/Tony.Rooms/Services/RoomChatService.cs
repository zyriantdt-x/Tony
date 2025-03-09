using Tony.Shared;
using Tony.Shared.Dto;
using Tony.Shared.Enums;
using Tony.Shared.Events.Rooms;

namespace Tony.Rooms.Services;

public class RoomChatService {
    private readonly PublisherService pub;
    private readonly RoomEntityService entities;

    public RoomChatService( PublisherService pub, RoomEntityService entities ) {
        this.pub = pub;
        this.entities = entities;
    }

    public async Task Chat( int room_id, int player_id, ChatType type, string message ) { 
        ICollection<RoomEntityDto> entities = await this.entities.GetEntitiesInRoom( room_id );
        await this.pub.Publish( new RoomChatEvent() {
            Audience = entities.Where( e => e.EntityType == EntityType.PLAYER ).Select( entity => entity.EntityId ).ToList(),
            SenderInstanceId = instance_id,
            Type = type,
            Message = message
        } );

    }
}
