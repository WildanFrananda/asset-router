namespace AssetRouter.Presentation.Components;

using AssetRouter.Application.Services;
using AssetRouter.Core.Entities;
using Microsoft.AspNetCore.Components;

public partial class StockCandidateList {
    [Inject]
    private StockScreenerService Screener { get; set; } = default!;

    private List<StockMetric> Candidates = new();
    private bool IsLoading = true;
    private string? LoadError;

    protected override async Task OnInitializedAsync() {
        try {
            Candidates = await Screener.GetCandidatesAsync();
        }
        catch (Exception ex) {
            LoadError = $"Fail to load stock data: {ex.Message}";
        }
        finally {
            IsLoading = false;
        }
    }
}
