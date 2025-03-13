using Tony.Sdk.Enums;

namespace Tony.Sdk.PubSub.Events.Player;
public class RoomChatEvent : EventBase {
    public override string Event => "room_chat";

    public int SenderInstanceId { get; set; }
    public ChatType Type { get; set; }
    public string Message { get; set; } = "nomsg";
}