﻿
using Tony.Shared.Enums;

namespace Tony.Shared.Events.Rooms;
public class RoomChatEvent : IEvent {
    public string Event => "room_chat";

    public required List<int> Audience { get; init; }

    public int SenderInstanceId { get; set; }
    public ChatType Type { get; set; }
    public string Message { get; set; } = "nomsg";
}