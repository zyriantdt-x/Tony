using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tony.Player.Storage.Entities;

[Table( "player_data" )]
public class PlayerData {
    [Column( "id" )] [Key] public required int Id { get; set; }
    [Column( "username" )] public required string Username { get; set; }
    [Column( "password" )] public required string Password { get; set; }
    [Column( "credits" )] public required int Credits { get; set; }
    [Column( "figure" )] public required string Figure { get; set; }
    [Column( "sex" )] public required bool Sex { get; set; } // 1 male 0 female
    [Column( "mission" )] public required string Mission { get; set; }
    [Column( "tickets" )] public required int Tickets { get; set; }
    [Column( "pool_figure" )] public required string PoolFigure { get; set; }
    [Column( "film" )] public required int Film { get; set; }
    [Column( "receive_news" )] public required bool ReceiveNews { get; set; }
}