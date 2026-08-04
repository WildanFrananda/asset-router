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
    protected string _gratificationToast = string.Empty;
    protected bool _showPaydaySuccessModal = false;

    protected decimal TotalAllocationPercent => State.CurrentNodes.Sum(n => n.Percentage);
    protected bool IsPercentageValid => Math.Abs(TotalAllocationPercent - 100m) < 0.01m;

    protected override async Task OnInitializedAsync() {
        _isLoading = true;
        State = await CommandCenterService.LoadCommandCenterStateAsync(15000000m);
        UpdateGratificationToast();
        _isLoading = false;
    }

    protected void HandleSalaryChanged(decimal newSalary) {
        State = CommandCenterService.RecalculateAll(newSalary, State.CurrentNodes, State.SelectedScenario);
        UpdateGratificationToast();
    }

    protected void HandleNodesUpdated(List<AllocationNode> updatedNodes) {
        State = CommandCenterService.RecalculateAll(State.MonthlySalary, updatedNodes, State.SelectedScenario);
        UpdateGratificationToast();
    }

    private void UpdateGratificationToast() {
        var investAmount = State.CurrentNodes
            .Where(n => n.Category != "Expenses" && n.Category != "Living" && n.Category != "Income")
            .Sum(n => n.AllocatedAmount);

        if (investAmount > 0) {
            var future15YrVal = investAmount * (decimal)Math.Pow(1 + 0.10, 15);
            var retiredMonthsEarlier = Math.Round((double)(investAmount / 500000m), 1);
            _gratificationToast = $"+Rp {future15YrVal:N0} in 2041! Retire {retiredMonthsEarlier} Months Early!";
        }
        else {
            _gratificationToast = string.Empty;
        }
    }

    protected async Task ExecutePaydayProtocolAsync() {
        _isSaving = true;
        await CommandCenterService.SaveNodePositionsAsync(State.CurrentNodes, State.MonthlySalary);
        await Task.Delay(400);
        _isSaving = false;
        _showPaydaySuccessModal = true;
    }

    protected void ClosePaydayModal() {
        _showPaydaySuccessModal = false;
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
        await CommandCenterService.SaveNodePositionsAsync(State.CurrentNodes, State.MonthlySalary);
        await Task.Delay(300);
        _isSaving = false;
    }

    protected async Task HandleNodeAdded(AllocationNode node) {
        await CommandCenterService.AddNodeAsync(node);
        State = await CommandCenterService.LoadCommandCenterStateAsync(State.MonthlySalary);
        UpdateGratificationToast();
    }

    protected async Task HandleNodeDeleted(Guid nodeId) {
        await CommandCenterService.DeleteNodeAsync(nodeId);
        State = await CommandCenterService.LoadCommandCenterStateAsync(State.MonthlySalary);
        UpdateGratificationToast();
    }

    protected async Task HandleNodesReset() {
        await CommandCenterService.ResetNodesAsync();
        State = await CommandCenterService.LoadCommandCenterStateAsync(State.MonthlySalary);
        UpdateGratificationToast();
    }
}