namespace AssetRouter.Presentation.Pages;

using System.Globalization;
using Microsoft.AspNetCore.Components;
using AssetRouter.Application.Services;
using AssetRouter.Core.Entities;

public partial class AnalyticsPage {
    [Inject] public PortfolioManagerService PortfolioManager { get; set; } = default!;

    protected bool _isLoading = true;
    protected List<AllocationSnapshot> HistorySnapshots { get; set; } = new();

    protected int DisciplineStreakMonths => Math.Max(1, HistorySnapshots.Count);
    protected decimal TotalAllocatedSalary => HistorySnapshots.Sum(s => s.Salary);
    protected decimal AverageSalary => HistorySnapshots.Any() ? HistorySnapshots.Average(s => s.Salary) : 0m;
    protected decimal MaxSalary => HistorySnapshots.Any() ? HistorySnapshots.Max(s => s.Salary) : 1m;

    protected override async Task OnInitializedAsync() {
        await LoadDataAsync();
    }

    protected async Task LoadDataAsync() {
        _isLoading = true;
        HistorySnapshots = await PortfolioManager.GetHistoryAsync();
        _isLoading = false;
    }

    protected static string FormatCurrency(decimal amount) {
        var culture = new CultureInfo("id-ID");
        return string.Format(culture, "Rp {0:N0}", amount);
    }

    protected static string FormatShortCurrency(decimal amount) {
        if (amount >= 1000000000m) return $"Rp {(amount / 1000000000m):N1}M";
        if (amount >= 1000000m) return $"Rp {(amount / 1000000m):N1}Jt";
        return $"Rp {(amount / 1000m):N0}rb";
    }
}