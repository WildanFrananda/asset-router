namespace AssetRouter.Presentation.Components;

using Microsoft.AspNetCore.Components;
using AssetRouter.Core.Entities;

public partial class TopCrisisBar {
    [Parameter] public List<StressTestScenario> Scenarios { get; set; } = new();
    [Parameter] public StressTestResult? ActiveResult { get; set; }
    [Parameter] public string SelectedScenarioId { get; set; } = "recession";
    [Parameter] public EventCallback<string> OnScenarioSelected { get; set; }
    [Parameter] public EventCallback OnRunSimulation { get; set; }

    private bool _showDrawer = false;

    private async Task OnScenarioChanged(ChangeEventArgs e) {
        var id = e.Value?.ToString() ?? "recession";
        await OnScenarioSelected.InvokeAsync(id);
    }

    private async Task OnRunSimulationClicked() {
        _showDrawer = true;
        await OnRunSimulation.InvokeAsync();
    }

    private void ToggleAlertsDrawer() => _showDrawer = !_showDrawer;

    protected static string GetScoreBadgeBackground(int score) => score switch {
        >= 80 => "linear-gradient(135deg, #10b981, #059669)",
        >= 50 => "linear-gradient(135deg, #f59e0b, #d97706)",
        _ => "linear-gradient(135deg, #ef4444, #dc2626)"
    };

    protected static string GetSurvivalLabel(int score) => score switch {
        >= 80 => "Excellent Resilience",
        >= 50 => "Moderate Exposure",
        _ => "High Risk Vulnerability"
    };
}