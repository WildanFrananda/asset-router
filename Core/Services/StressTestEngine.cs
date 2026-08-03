namespace AssetRouter.Core.Services;

using AssetRouter.Core.Entities;

public class StressTestEngine {
    public List<StressTestScenario> GetAvailableScenarios() {
        return new List<StressTestScenario> {
            new StressTestScenario {
                Id = "recession",
                Title = "📉 High Inflation & Recession",
                Description = "Simulates 8% annual inflation and a 30% stock market crash.",
                InflationRateModifier = 0.08m,
                StockMarketDropPercentage = 30.0m,
                IncomeReductionPercentage = 0.0m,
                MonthsToSimulate = 12
            },
            new StressTestScenario {
                Id = "job_loss",
                Title = "🏥 Job Loss & Income Disruption",
                Description = "Simulates 100% total income loss for 6 to 12 months.",
                InflationRateModifier = 0.04m,
                StockMarketDropPercentage = 15.0m,
                IncomeReductionPercentage = 100.0m,
                MonthsToSimulate = 12
            },
            new StressTestScenario {
                Id = "bull_market",
                Title = "🚀 Bull Market & Economic Expansion",
                Description = "Simulates strong economic growth with high asset appreciation.",
                InflationRateModifier = 0.03m,
                StockMarketDropPercentage = -20.0m, // Negative value = 20% asset appreciation                                                                                                          
                IncomeReductionPercentage = 0.0m,
                MonthsToSimulate = 12
            }
        };
    }

    public StressTestResult RunSimulation(StressTestScenario scenario, List<AllocationNode> nodes, decimal monthlySalary) {
        var emergencyNode = nodes.FirstOrDefault(n => n.Category == "Emergency");
        var expenseNode = nodes.FirstOrDefault(n => n.Category == "Expenses");
        var stocksNode = nodes.FirstOrDefault(n => n.Category == "Stocks");
        var cryptoNode = nodes.FirstOrDefault(n => n.Category == "Crypto");

        decimal monthlyExpense = expenseNode != null
            ? (expenseNode.AllocatedAmount > 0 ? expenseNode.AllocatedAmount : monthlySalary * (expenseNode.Percentage / 100m))
            : monthlySalary * 4.0m;

        decimal adjustedMonthlyExpense = monthlyExpense * (1.0m + scenario.InflationRateModifier);

        decimal emergencyFundBalance = emergencyNode?.CurrentAccumulatedAmount ?? 0m;

        int runwayMonths = adjustedMonthlyExpense > 0
            ? (int)Math.Floor(emergencyFundBalance / adjustedMonthlyExpense)
            : 0;

        decimal stockValue = stocksNode?.AllocatedAmount ?? 0m;
        decimal cryptoValue = cryptoNode?.AllocatedAmount ?? 0m;
        decimal projectedDrop = 0m;

        if (scenario.StockMarketDropPercentage > 0) {
            projectedDrop += stockValue * (scenario.StockMarketDropPercentage / 100m);
            projectedDrop += cryptoValue * (Math.Min(100m, scenario.StockMarketDropPercentage * 1.5m) / 100m);
        }

        int survivalScore = 100;

        if (runwayMonths < 3) survivalScore -= 40;
        else if (runwayMonths < 6) survivalScore -= 20;

        if (cryptoNode != null && cryptoNode.Percentage > 20m) survivalScore -= 15;
        if (emergencyFundBalance < (adjustedMonthlyExpense * 6)) survivalScore -= 15;

        survivalScore = Math.Clamp(survivalScore, 10, 100);

        var alerts = new List<string>();
        var recommendations = new List<string>();

        if (runwayMonths < 6) {
            alerts.Add($"⚠️ Emergency fund can only sustain {runwayMonths} months of expenses during crisis (Below 6-month safety threshold).");
            recommendations.Add("💡 Increase Emergency Fund node allocation to at least 25% until target capacity is met.");
        }

        if (scenario.IncomeReductionPercentage >= 50m && runwayMonths < 12) {
            alerts.Add("⚠️ High layoff risk: Runway under 12 months threatens financial stability.");
            recommendations.Add("💡 Enable overflow valves from high-risk instruments directly into Emergency Fund.");
        }

        if (cryptoNode != null && cryptoNode.Percentage > 15m && scenario.StockMarketDropPercentage > 0) {
            alerts.Add($"⚠️ Crypto node allocation ({cryptoNode.Percentage}%) is highly vulnerable during market downturns.");
            recommendations.Add("💡 Reduce Crypto node percentage to maximum 10% and reallocate to Gold as hedging.");
        }

        return new StressTestResult {
            ScenarioId = scenario.Id,
            ScenarioTitle = scenario.Title,
            SurvivalScore = survivalScore,
            RunwayMonths = runwayMonths,
            IsEmergencyFundSufficient = runwayMonths >= 6,
            ProjectedValueDrop = projectedDrop,
            RiskAlerts = alerts,
            RecommendedValveAdjustments = recommendations
        };
    }
}