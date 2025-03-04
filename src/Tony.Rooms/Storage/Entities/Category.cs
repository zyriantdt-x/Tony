using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tony.Rooms.Storage.Entities;

[Table( "navigator_categories" )]
public class Category {
    [Column( "id" )] [Key] public required int Id { get; set; }
    [Column( "parent_id" )] public required int ParentId { get; set; }
    [Column( "name" )] public required string Name { get; set; }
    [Column( "is_node" )] public required bool IsNode { get; set; }
    [Column( "is_public_space" )] public required bool IsPublicSpace { get; set; }
    [Column( "is_trading_allowed" )] public required bool IsTradingAllowed { get; set; }
    [Column( "min_access" )] public required int MinAccess { get; set; }
    [Column( "min_assign" )] public required int MinAssign { get; set; }
}