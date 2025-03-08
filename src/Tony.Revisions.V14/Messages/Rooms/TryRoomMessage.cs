using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Messages.Rooms;
public class TryRoomMessage
{
    public int RoomId { get; set; }
    public string? Password { get; set; }
}
