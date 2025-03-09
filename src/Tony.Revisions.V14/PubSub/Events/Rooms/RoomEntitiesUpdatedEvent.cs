using Tony.Sdk.Dto;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Events.Rooms;
public class RoomEntitiesUpdatedEvent : EventBase {
    public override string Event => "room_entities_updated";

    public required IEnumerable<RoomEntityDto> Entities { get; set; }
}
