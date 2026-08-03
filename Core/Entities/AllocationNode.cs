namespace AssetRouter.Core.Entities;

public class AllocationNode {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Percentage { get; set; }

    public decimal AllocatedAmount { get; set; }

    public decimal TargetCapAmount { get; set; }

    public decimal CurrentAccumulatedAmount { get; set; }

    public bool IsOverflowEnabled { get; set; } = true;

    public Guid? OverflowTargetNodeId { get; set; }

    public double X { get; set; }
    public double Y { get; set; }

    public bool IsCapReached => TargetCapAmount > 0 && CurrentAccumulatedAmount >= TargetCapAmount;

    public decimal FillPercentage => TargetCapAmount > 0
        ? Math.Min(100m, (CurrentAccumulatedAmount / TargetCapAmount) * 100m)
        : 0m;
}
