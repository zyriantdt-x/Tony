using Tony.Revisions.V14.Messages.Rooms;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Parsers.Rooms;
[Header(21)]
public class GetRoomInfoParser : IParser<GetRoomInfoMessage>
{
    public GetRoomInfoMessage Parse(Message message)
        => new() { RoomId = Convert.ToInt32(System.Text.Encoding.Default.GetString(message.RemainingBytes)) };
}
