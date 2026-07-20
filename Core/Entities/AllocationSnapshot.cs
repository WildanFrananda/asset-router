namespace AssetRouter.Core.Entities;

public class AllocationSnapshot {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal Salary { get; set; }
    public List<AssetAllocation> Items { get; set; } = new();
}
