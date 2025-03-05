using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table( "room_models" )]
public class RoomModel {
    [Column( "model_id" )] [Key] public required string Id { get; set; } // makes little sense to have an ID and a Model Id
    [Column( "door_x" )] public required int DoorX { get; set; }
    [Column( "door_y" )] public required int DoorY { get; set; }
    [Column( "door_z" )] public required int DoorZ { get; set; }
    [Column( "door_dir" )] public required int DoorDir { get; set; }
    [Column( "heightmap" )] public required string Heightmap { get; set; }
    [Column( "trigger_class" )] public required string TriggerClass { get; set; }
}