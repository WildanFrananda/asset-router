namespace AssetRouter.Core.Entities;

public record AssetAllocation {
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Category { get; init; } = string.Empty;
    public decimal Percentage { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}