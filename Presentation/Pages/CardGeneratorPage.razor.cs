namespace AssetRouter.Presentation.Pages;

using AssetRouter.Application.Services;
using Microsoft.AspNetCore.Components;

public partial class CardGeneratorPage {
    [Inject] public CommandCenterService CommandCenterService { get; set; } = default!;

    protected CommandState State { get; private set; } = new();
    protected string SelectedTheme { get; set; } = "cyber";
    protected string UserTag { get; set; } = "@financial_discipline";
    protected string CustomTagline { get; set; } = "Allocating first upon payday. Zero impulse spending!";
    protected bool _copiedToast = false;
    protected bool _isLoading = true;

    protected override async Task OnInitializedAsync() {
        _isLoading = true;
        State = await CommandCenterService.LoadCommandCenterStateAsync(15000000m);
        _isLoading = false;
    }

    protected void SelectTheme(string theme) {
        SelectedTheme = theme;
    }

    protected async Task CopySummaryToClipboard() {
        _copiedToast = true;
        await Task.Delay(2500);
        _copiedToast = false;
    }
}