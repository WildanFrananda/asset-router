namespace AssetRouter.Core.Entities;

public class YearlyWealthSnapshot {
    public int Year { get; set; }
    public decimal TotalWealth { get; set; }
    public decimal TotalInvested { get; set; }
    public decimal InvestmentReturns { get; set; }

    public Dictionary<string, decimal> BucketAmounts { get; set; } = new();
}

public class TimelineUniverse {
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string BadgeColor { get; set; } = "info";

    public int EstimatedRetirementAge { get; set; }

    public List<YearlyWealthSnapshot> Snapshots { get; set; } = new();

    public decimal FinalProjectedWealth => Snapshots.LastOrDefault()?.TotalWealth ?? 0m;
}