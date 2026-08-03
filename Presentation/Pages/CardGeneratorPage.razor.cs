namespace AssetRouter.Presentation.Pages;

using Microsoft.AspNetCore.Components;

public partial class CardGeneratorPage {
    protected string SelectedTheme { get; set; } = "cyber";
    protected string UserTag { get; set; } = "@financial_discipline";
    protected string CustomTagline { get; set; } = "Allocating first upon payday. Zero impulse spending!";
    protected bool _copiedToast = false;

    protected void SelectTheme(string theme) {
        SelectedTheme = theme;
    }

    protected async Task CopySummaryToClipboard() {
        _copiedToast = true;
        await Task.Delay(2500);
        _copiedToast = false;
    }
}