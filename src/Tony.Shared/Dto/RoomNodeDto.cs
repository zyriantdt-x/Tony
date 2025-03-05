﻿namespace Tony.Shared.Dto;
public class RoomNodeDto {
    public int Id { get; set; }
    public bool IsPublicRoom { get; set; }
    public string Name { get; set; } = "noname";
    public string Description { get; set; } = "nodesc";
    public int VisitorsNow { get; set; }
    public int VisitorsMax { get; set; }
    public int CategoryId { get; set; }
    public string Ccts { get; set; } = "nocct";
    public string OwnerName { get; set; } = "herobrine";
    public AccessType AccessType { get; set; }
}

public enum AccessType {
    OPEN,
    CLOSED,
    PASSWORD
}