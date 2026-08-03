namespace AssetRouter.Infrastructure.DataSources;

using System.Net.Http.Json;
using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;

public class HttpStockDAtaSource(HttpClient http) : IStockDataSource {
    public async Task<List<StockMetric>> GetStocksAsync() {
        return await http.GetFromJsonAsync<List<StockMetric>>("data/stocks.json") ?? [];
    }
}
