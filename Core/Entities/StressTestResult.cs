namespace AssetRouter.Core.Entities;

public class StressTestResult {
    public string ScenarioId { get; set; } = string.Empty;
    public string ScenarioTitle { get; set; } = string.Empty;

    public int SurvivalScore { get; set; }

    public int RunwayMonths { get; set; }

    public bool IsEmergencyFundSufficient { get; set; }

    public decimal ProjectedValueDrop { get; set; }

    public List<string> RiskAlerts { get; set; } = new();

    public List<string> RecommendedValveAdjustments { get; set; } = new();
}