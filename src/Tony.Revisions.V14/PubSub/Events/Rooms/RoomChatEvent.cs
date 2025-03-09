using Tony.Sdk.Enums;
using Tony.Sdk.Revisions.PubSub;

namespace Tony.Revisions.V14.PubSub.Events.Rooms;
public class RoomChatEvent : EventBase {
    public override string Event => "room_chat";

    public int SenderInstanceId { get; set; }
    public ChatType Type { get; set; }
    public string Message { get; set; } = "nomsg";
}