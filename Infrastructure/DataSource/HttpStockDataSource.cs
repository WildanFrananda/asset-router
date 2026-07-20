namespace AssetRouter.Infrastructure.DataSources;

using System.Net.Http.Json;
using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;

public class HttpStockDAtaSource(HttpClient http) : IStockDataSource {
    public async Task<List<StockMetric>> GetStocksAsync() {
        return await http.GetFromJsonAsync<List<StockMetric>>("data/stocks.json") ?? [];
    }
}
