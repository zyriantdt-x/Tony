using Tony.Sdk.Dto;

namespace Tony.Sdk.PubSub.Events.Player;
public class RoomEntitiesUpdatedEvent : EventBase {
    public override string Event => "room_entities_updated";

    public required IEnumerable<RoomEntityDto> Entities { get; set; }
}
