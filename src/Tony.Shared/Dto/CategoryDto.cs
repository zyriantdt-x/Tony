namespace Tony.Shared.Dto;

public class CategoryDto {
    public required int Id { get; set; }
    public required int ParentId { get; set; }
    public required string Name { get; set; }
    public required bool IsNode { get; set; }
    public required bool IsPublicSpace { get; set; }
    public required bool IsTradingAllowed { get; set; }
    public required int MinAccess { get; set; }
    public required int MinAssign { get; set; }
}
