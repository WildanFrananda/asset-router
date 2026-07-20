namespace AssetRouter.Presentation.Components;

using Microsoft.AspNetCore.Components;
using AssetRouter.Core.Entities;
using AssetRouter.Application.Services;

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
