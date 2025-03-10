﻿using Tony.Sdk.Enums;

namespace Tony.Sdk.Dto;
public class RoomDataDto {
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = "noowner";
    public int Category { get; set; }
    public string Name { get; set; } = "noname";
    public string Description { get; set; } = "nodesc";
    public string ModelId { get; set; } = "model_a";
    public RoomModelDto Model { get; set; } = new();
    public string? Ccts { get; set; }
    public int Wallpaper { get; set; }
    public int Floor { get; set; }
    public bool ShowName { get; set; }
    public bool SuperUsers { get; set; }
    public AccessType AccessType { get; set; }
    public string? Password { get; set; }
    public int VisitorsNow { get; set; }
    public int VisitorsMax { get; set; }
    public int Rating { get; set; }
    public bool IsHidden { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
