namespace AssetRouter.Core.Entities;

public class EmergencyFundStatus {
    public decimal Accumulated { get; set; }
    public decimal MonthlyExpense { get; set; }
    public decimal Target { get; set; }
    public bool IsTargetReached => Target > 0 && Accumulated >= Target;
    public decimal ProgressPercent => Target <= 0 ? 0 : Math.Min(100m, Accumulated / Target * 100m);
    public string? SuggestedBucket { get; set; }
}