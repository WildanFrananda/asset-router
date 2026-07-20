namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IStockDataSource {
    Task<List<StockMetric>> GetStocksAsync();
}
