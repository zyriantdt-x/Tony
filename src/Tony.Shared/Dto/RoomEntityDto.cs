namespace Tony.Shared.Dto;
public class RoomEntityDto {
    public int InstanceId { get; set; } // could be user id, not sure yet
    public int EntityId { get; set; } // haven't decided yet
    public EntityType EntityType { get; set; }
    public string Username { get; set; } = "noname";
    public string Figure { get; set; } = "nofigure"; // we ought to change this to a default figure
    public string Sex { get; set; } = "m";
    public string Motto { get; set; } = "nomotto";
    public string Badge { get; set; } = "BOT";

    public int PosX { get; set; }
    public int PosY { get; set; }
    public double PosZ { get; set; }
}

public enum EntityType {
    PLAYER,
    PET,
    BOT
}