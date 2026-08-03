namespace AssetRouter.Core.Entities;

public class StressTestScenario {
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal InflationRateModifier { get; set; }

    public decimal StockMarketDropPercentage { get; set; }

    public decimal IncomeReductionPercentage { get; set; }

    public int MonthsToSimulate { get; set; } = 12;
}