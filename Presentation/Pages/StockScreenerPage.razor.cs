namespace AssetRouter.Presentation.Pages;

using Microsoft.AspNetCore.Components;
using AssetRouter.Application.Services;
using AssetRouter.Core.Entities;

public partial class StockScreenerPage {
    [Inject] public StockScreenerService ScreenerService { get; set; } = default!;

    protected bool _isLoading = true;
    protected List<StockMetric> AllCandidates { get; set; } = new();
    protected StockMetric? SelectedCandidate { get; set; }

    protected string SelectedSector { get; set; } = "All";
    protected decimal MaxPE { get; set; } = 15m;
    protected decimal MaxDER { get; set; } = 1.0m;
    protected decimal MinROE { get; set; } = 15m;

    protected List<string> AvailableSectors => AllCandidates
        .Select(s => s.Sector)
        .Distinct()
        .OrderBy(s => s)
        .ToList();

    protected List<StockMetric> FilteredCandidates => AllCandidates
        .Where(s => (SelectedSector == "All" || s.Sector == SelectedSector) &&
                    s.PriceToEarnings <= MaxPE &&
                    s.DebtToEquity <= MaxDER &&
                    s.ReturnOnEquity >= MinROE)
        .OrderByDescending(s => s.ReturnOnEquity)
        .ToList();

    protected override async Task OnInitializedAsync() {
        _isLoading = true;
        AllCandidates = await ScreenerService.GetCandidatesAsync();
        SelectedCandidate = FilteredCandidates.FirstOrDefault();
        _isLoading = false;
    }

    protected void OnSectorChanged(ChangeEventArgs e) {
        SelectedSector = e.Value?.ToString() ?? "All";
        SelectedCandidate = FilteredCandidates.FirstOrDefault();
    }

    protected void OnMaxPEInput(ChangeEventArgs e) {
        if (decimal.TryParse(e.Value?.ToString(), out decimal val)) MaxPE = val;
    }

    protected void OnMaxDERInput(ChangeEventArgs e) {
        if (decimal.TryParse(e.Value?.ToString(), out decimal val)) MaxDER = val;
    }

    protected void OnMinROEInput(ChangeEventArgs e) {
        if (decimal.TryParse(e.Value?.ToString(), out decimal val)) MinROE = val;
    }

    protected void SelectCandidate(StockMetric stock) {
        SelectedCandidate = stock;
    }

    protected string GetRadarPoints(StockMetric stock) {
        double peFactor = Math.Clamp((double)(25m - stock.PriceToEarnings) / 20.0, 0.2, 1.0);
        double roeFactor = Math.Clamp((double)stock.ReturnOnEquity / 35.0, 0.2, 1.0);
        double derFactor = Math.Clamp((double)(2.0m - stock.DebtToEquity) / 2.0, 0.2, 1.0);
        double divFactor = Math.Clamp((double)stock.DividendYield / 10.0, 0.2, 1.0);

        double x1 = 150;
        double y1 = 150 - (100 * peFactor);

        double x2 = 150 + (100 * roeFactor * 0.86);
        double y2 = 150 - (100 * roeFactor * 0.5);

        double x3 = 150 + (80 * divFactor * 0.7);
        double y3 = 150 + (80 * divFactor * 0.7);

        double x4 = 150 - (80 * derFactor * 0.7);
        double y4 = 150 + (80 * derFactor * 0.7);

        double x5 = 150 - (90 * 0.8);
        double y5 = 150 - (90 * 0.4);

        return $"{x1},{y1} {x2},{y2} {x3},{y3} {x4},{y4} {x5},{y5}";
    }
}