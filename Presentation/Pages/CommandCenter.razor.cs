namespace AssetRouter.Presentation.Pages;

using Microsoft.AspNetCore.Components;
using AssetRouter.Application.Services;
using AssetRouter.Core.Entities;

public partial class CommandCenter {
    [Inject] public CommandCenterService CommandCenterService { get; set; } = default!;

    protected CommandState State { get; private set; } = new();

    protected bool _isLoading = true;
    protected bool _isSaving = false;
    protected string _activeUniverseId = "UniverseB_Balanced";
    protected int _selectedYear = 2026;

    protected decimal TotalAllocationPercent => State.CurrentNodes.Sum(n => n.Percentage);
    protected bool IsPercentageValid => Math.Abs(TotalAllocationPercent - 100m) < 0.01m;

    protected override async Task OnInitializedAsync() {
        _isLoading = true;
        State = await CommandCenterService.LoadCommandCenterStateAsync(15000000m); // Rp 15M initial salary                                                                                             
        _isLoading = false;
    }

    protected void HandleSalaryChanged(decimal newSalary) {
        State = CommandCenterService.RecalculateAll(newSalary, State.CurrentNodes, State.SelectedScenario);
    }

    protected void HandleNodesUpdated(List<AllocationNode> updatedNodes) {
        State = CommandCenterService.RecalculateAll(State.MonthlySalary, updatedNodes, State.SelectedScenario);
    }

    protected void HandleScenarioSelected(string scenarioId) {
        var scenarios = CommandCenterService.GetCrisisScenarios();
        var selected = scenarios.FirstOrDefault(s => s.Id == scenarioId) ?? scenarios.First();
        State = CommandCenterService.RecalculateAll(State.MonthlySalary, State.CurrentNodes, selected);
    }

    protected void HandleRunSimulation() {
        State = CommandCenterService.RecalculateAll(State.MonthlySalary, State.CurrentNodes, State.SelectedScenario);
    }

    protected void HandleUniverseSelected(string universeId) {
        _activeUniverseId = universeId;
    }

    protected void HandleYearSelected(int year) {
        _selectedYear = year;
    }

    protected async Task SaveLayoutAsync() {
        _isSaving = true;
        await CommandCenterService.SaveNodePositionsAsync(State.CurrentNodes);
        await Task.Delay(300); // UI feel                                                                                                                                                               
        _isSaving = false;
    }
}