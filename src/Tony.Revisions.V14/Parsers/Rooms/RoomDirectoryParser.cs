using Tony.Revisions.V14.Messages.Rooms;

using Tony.Sdk.Revisions; namespace Tony.Revisions.V14.Parsers.Rooms;
[Header(2)]
public class RoomDirectoryParser : IParser<RoomDirectoryMessage>
{
    public RoomDirectoryMessage Parse(Message message)
        => new()
        {
            IsPublic = System.Text.Encoding.Default.GetString(message.RemainingBytes.ToArray())[ 0 ] == 'A'
        };
}
