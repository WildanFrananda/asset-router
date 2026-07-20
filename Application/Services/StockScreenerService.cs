namespace AssetRouter.Application.Services;

using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;

public class StockScreenerService(IStockDataSource dataSource) {
    public const decimal MaxDebtToEquity = 1.0m;
    public const decimal MinReturnOnEquity = 15.0m;

    public async Task<List<StockMetric>> GetCandidatesAsync() {
        var stocks = await dataSource.GetStocksAsync();

        var sectorAveragePe = stocks
            .GroupBy(s => s.Sector)
            .ToDictionary(g => g.Key, g => g.Average(s => s.PriceToEarnings));

        foreach (var stock in stocks) {
            stock.PassesFilter =
                stock.PriceToEarnings > 0 &&
                stock.PriceToEarnings < sectorAveragePe[stock.Sector] &&
                stock.DebtToEquity < MaxDebtToEquity &&
                stock.ReturnOnEquity > MinReturnOnEquity;
        }

        return stocks
            .Where(s => s.PassesFilter)
            .OrderByDescending(s => s.ReturnOnEquity)
            .ToList();
    }
}
