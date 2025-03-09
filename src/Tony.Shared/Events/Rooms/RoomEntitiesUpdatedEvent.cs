
using Tony.Shared.Dto;

namespace Tony.Shared.Events.Rooms;
public class RoomEntitiesUpdatedEvent : IEvent {
    public string Event => "ROOM_ENTITIES_UPDATED";

    public required List<int> Audience { get; init; }

    public required IEnumerable<RoomEntityDto> Entities { get; set; }
}
