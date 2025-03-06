namespace Tony.Shared.Dto;
public class RoomModelDto {
    public string Id { get; set; } = "noid";
    public int DoorX { get; set; }
    public int DoorY { get; set; }
    public int DoorZ { get; set; }
    public int DoorDir { get; set; }
    public string Heightmap { get; set; } = "nohmap";
    public string TriggerClass { get; set; } = "notriggerclass";
}
