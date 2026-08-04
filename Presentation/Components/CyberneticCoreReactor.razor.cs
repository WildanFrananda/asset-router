namespace AssetRouter.Presentation.Components;

using Microsoft.AspNetCore.Components;
using AssetRouter.Core.Entities;

public partial class CyberneticCoreReactor {
    [Parameter] public List<AllocationNode> Nodes { get; set; } = new();
    [Parameter] public decimal MonthlySalary { get; set; } = 15000000m;
    [Parameter] public string GratificationToast { get; set; } = string.Empty;

    protected string CoreState { get; set; } = "overdrive";
    protected string StateIcon { get; set; } = "⚡";
    protected string StateTitle { get; set; } = "CORE STABLE: OVERDRIVE";
    protected string StateSubtext { get; set; } = "High Investment Velocity • Optimal Wealth Acceleration";
    protected int PowerPercentage { get; set; } = 100;

    protected override void OnParametersSet() {
        CalculateCoreState();
    }

    private void CalculateCoreState() {
        if (Nodes == null || Nodes.Count == 0) return;

        var investPercent = Nodes
            .Where(n => n.Category != "Expenses" && n.Category != "Living" && n.Category != "Income")
            .Sum(n => n.Percentage);

        var expensePercent = Nodes
            .Where(n => n.Category == "Expenses" || n.Category == "Living")
            .Sum(n => n.Percentage);

        if (expensePercent > 65m) {
            CoreState = "meltdown";
            StateIcon = "🚨";
            StateTitle = "CRITICAL MELTDOWN WARNING";
            StateSubtext = "High Lifestyle Inflation Risk • Low Defense Capacity";
            PowerPercentage = Math.Max(20, (int)(100 - expensePercent));
        }
        else if (investPercent >= 30m) {
            CoreState = "overdrive";
            StateIcon = "⚡";
            StateTitle = "OVERDRIVE 100% STABLE";
            StateSubtext = "Supercharged Wealth Routing • High Compounding Power";
            PowerPercentage = 100;
        }
        else {
            CoreState = "balanced";
            StateIcon = "🛡️";
            StateTitle = "BALANCED POWER CORE";
            StateSubtext = "Moderate Wealth Routing • Steady Growth Matrix";
            PowerPercentage = (int)(investPercent * 2.5m + 25m);
        }
    }
}