namespace Tony.Sdk.Dto;
public class NavNodeDto {
    public int Id { get; set; }
    public bool IsPublicRoom { get; set; }
    public string Name { get; set; } = "noname";
    public string Description { get; set; } = "nodesc";
    public int VisitorsNow { get; set; }
    public int VisitorsMax { get; set; }
    public int CategoryId { get; set; }
    public string Ccts { get; set; } = "nocct";
    public string OwnerName { get; set; } = "herobrine"; // this is set by the navigate handler, we will come up with a better way to deal with this down the line
    public AccessType AccessType { get; set; }

    public int OwnerId { get; set; }
}

public enum AccessType {
    OPEN,
    CLOSED,
    PASSWORD
}