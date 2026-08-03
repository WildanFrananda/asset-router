namespace AssetRouter.Core.Services;

using AssetRouter.Core.Entities;

public class TimelineProjectionEngine {
    private const int StartYear = 2026;
    private const int ProjectionYears = 20;

    public List<TimelineUniverse> GenerateParallelUniverses(decimal monthlySalary, List<AllocationNode> currentNodes) {
        decimal annualSalary = monthlySalary * 12m;

        return new List<TimelineUniverse> {
            GenerateStatusQuoUniverse(annualSalary),
            GenerateBalancedUniverse(annualSalary, currentNodes),
            GenerateAggressiveFIREUniverse(annualSalary, currentNodes)
        };
    }

    private static TimelineUniverse GenerateStatusQuoUniverse(decimal annualSalary) {
        var universe = new TimelineUniverse {
            Id = "UniverseA_StatusQuo",
            Name = "Universe A: Status Quo",
            Description = "Traditional reactive spending pattern (80% consumption, 5% passive savings).",
            BadgeColor = "warning",
            EstimatedRetirementAge = 65
        };

        decimal currentWealth = 0m;
        decimal totalInvested = 0m;
        decimal annualSavings = annualSalary * 0.05m;
        decimal annualReturnRate = 0.03m;

        for (int i = 0; i < ProjectionYears; i++) {
            int year = StartYear + i;
            totalInvested += annualSavings;
            currentWealth = (currentWealth + annualSavings) * (1.0m + annualReturnRate);

            universe.Snapshots.Add(new YearlyWealthSnapshot {
                Year = year,
                TotalWealth = Math.Round(currentWealth, 2),
                TotalInvested = Math.Round(totalInvested, 2),
                InvestmentReturns = Math.Round(currentWealth - totalInvested, 2),
                BucketAmounts = new Dictionary<string, decimal> {
                    { "Savings", Math.Round(currentWealth, 2) }
                }
            });
        }

        return universe;
    }

    private static TimelineUniverse GenerateBalancedUniverse(decimal annualSalary, List<AllocationNode> nodes) {
        var universe = new TimelineUniverse {
            Id = "UniverseB_Balanced",
            Name = "Universe B: Balanced Router",
            Description = "Disciplined 50/30/20 allocation rules with balanced growth.",
            BadgeColor = "info",
            EstimatedRetirementAge = 50
        };

        decimal totalWealth = 0m;
        decimal totalInvested = 0m;

        decimal investedPercentage = nodes
            .Where(n => n.Category is "Stocks" or "Gold" or "Crypto" or "Emergency")
            .Sum(n => n.Percentage);

        if (investedPercentage <= 0) investedPercentage = 30m;

        decimal annualInvestment = annualSalary * (investedPercentage / 100m);
        decimal blendedReturnDate = 0.07m;

        for (int i = 0; i < ProjectionYears; i++) {
            int year = StartYear + i;
            totalInvested += annualInvestment;
            totalWealth = (totalWealth + annualInvestment) * (1.0m + blendedReturnDate);

            universe.Snapshots.Add(new YearlyWealthSnapshot {
                Year = year,
                TotalWealth = Math.Round(totalWealth, 2),
                TotalInvested = Math.Round(totalInvested, 2),
                InvestmentReturns = Math.Round(totalWealth - totalInvested, 2),
                BucketAmounts = new Dictionary<string, decimal> {
                    { "Emergency", Math.Round(totalWealth * 0.25m, 2) },
                    { "Investments", Math.Round(totalWealth * 0.75m, 2) }
                }
            });
        }

        return universe;
    }

    private static TimelineUniverse GenerateAggressiveFIREUniverse(decimal annualSalary, List<AllocationNode> nodes) {
        var universe = new TimelineUniverse {
            Id = "UniverseC_AggressiveFIRE",
            Name = "Universe C: Aggressive FIRE Router",
            Description = "Optimized allocation + auto-overflow valves to high-growth assets.",
            BadgeColor = "success",
            EstimatedRetirementAge = 40
        };

        decimal totalWealth = 0m;
        decimal totalInvested = 0m;

        decimal investmentPercent = nodes
            .Where(n => n.Category is "Stocks" or "Gold" or "Crypto")
            .Sum(n => n.Percentage);

        if (investmentPercent <= 0) investmentPercent = 45m;
        else investmentPercent += 10m;

        decimal annualInvestment = annualSalary * (investmentPercent / 100m);
        decimal aggressiveReturnRate = 0.105m;

        for (int i = 0; i < ProjectionYears; i++) {
            int year = StartYear + i;
            totalInvested += annualInvestment;
            totalWealth = (totalWealth + annualInvestment) * (1.0m + aggressiveReturnRate);

            universe.Snapshots.Add(new YearlyWealthSnapshot {
                Year = year,
                TotalWealth = Math.Round(totalWealth, 2),
                TotalInvested = Math.Round(totalInvested, 2),
                InvestmentReturns = Math.Round(totalWealth - totalInvested, 2),
                BucketAmounts = new Dictionary<string, decimal> {
                    { "Emergency (Capped)", Math.Round(annualSalary * 0.5m, 2) },
                    { "Stocks (Fundamental)", Math.Round(totalWealth * 0.6m, 2) },
                    { "Gold & Crypto", Math.Round(totalWealth * 0.35m, 2) }
                }
            });
        }

        return universe;
    }
}
