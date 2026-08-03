namespace AssetRouter.Presentation.Pages;

using AssetRouter.Application.Services;
using AssetRouter.Core.Entities;
using Microsoft.AspNetCore.Components;

public partial class Home {
    [Inject]
    private PortfolioManagerService PortfolioManager { get; set; } = default!;

    private List<AllocationRule> Rules = new();
    private List<AllocationSnapshot> History = new();
    private string SelectedPreset = "";
    private decimal InputSalary;
    private string? RulesSavedMessage;
    private string? ErrorMessage;
    private EmergencyFundStatus? FundStatus;

    private IEnumerable<AssetAllocation>? CurrentResults;

    private decimal TotalPercentage => Rules.Sum(r => r.Percentage);
    private bool IsTotalValid => Rules.Count > 0 && TotalPercentage == 100m;
    private bool CanCalculate => InputSalary > 0 && IsTotalValid;

    protected override async Task OnInitializedAsync() {
        Rules = await PortfolioManager.GetRulesAsync();
        await LoadHistory();
    }

    private void ApplyPreset() {
        Rules = PortfolioManager.GetPresetRules(SelectedPreset);
        RulesSavedMessage = null;
    }

    private async Task SaveRules() {
        try {
            ErrorMessage = null;
            await PortfolioManager.SaveRulesAsync(Rules);
            RulesSavedMessage = "Aturan tersimpan.";
        }
        catch (Exception ex) {
            RulesSavedMessage = null;
            ErrorMessage = ex.Message;
        }
    }

    private async Task CalculateAndSave() {
        try {
            ErrorMessage = null;
            await PortfolioManager.SaveRulesAsync(Rules);
            var snapshot = await PortfolioManager.GenerateAndSavePortfolioAsync(InputSalary, Rules);
            CurrentResults = snapshot.Items;
            await LoadHistory();
        }
        catch (Exception ex) {
            ErrorMessage = ex.Message;
        }
    }

    private async Task LoadHistory() {
        History = await PortfolioManager.GetHistoryAsync();
        FundStatus = await PortfolioManager.GetEmergencyFundStatusAsync(Rules);
    }
}
