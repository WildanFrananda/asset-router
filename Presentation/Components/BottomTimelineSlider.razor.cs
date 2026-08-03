namespace AssetRouter.Presentation.Components;

using System.Globalization;
using Microsoft.AspNetCore.Components;
using AssetRouter.Core.Entities;

public partial class BottomTimelineSlider {
    [Parameter] public List<TimelineUniverse> Universes { get; set; } = new();
    [Parameter] public string ActiveUniverseId { get; set; } = "Universe_Balanced";
    [Parameter] public int SelectedYear { get; set; } = 2026;
    [Parameter] public EventCallback<string> OnUniverseSelected { get; set; }
    [Parameter] public EventCallback<int> OnYearSelected { get; set; }

    protected TimelineUniverse? ActiveUniverse =>
        Universes.FirstOrDefault(u => u.Id == ActiveUniverseId) ?? Universes.FirstOrDefault();

    protected YearlyWealthSnapshot? CurrentSnapshot =>
        ActiveUniverse?.Snapshots.FirstOrDefault(s => s.Year == SelectedYear)
        ?? ActiveUniverse?.Snapshots.FirstOrDefault();

    private async Task SelectUniverse(string universeId) {
        await OnUniverseSelected.InvokeAsync(universeId);
    }

    private async Task HandleYearInput(ChangeEventArgs e) {
        if (int.TryParse(e.Value?.ToString(), out int year)) {
            await OnYearSelected.InvokeAsync(year);
        }
    }

    protected static string FormatCurrency(decimal amount) {
        var culture = new CultureInfo("id-ID");
        return string.Format(culture, "Rp {0:N0}", amount);
    }
}