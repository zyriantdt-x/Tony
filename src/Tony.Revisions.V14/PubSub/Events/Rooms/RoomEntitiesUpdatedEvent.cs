using Tony.Sdk.Dto;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Events.Rooms;
public class RoomEntitiesUpdatedEvent : IEvent {
    public string Event => "ROOM_ENTITIES_UPDATED";

    public required List<int> Audience { get; init; }

    public required IEnumerable<RoomEntityDto> Entities { get; set; }
}
