namespace AssetRouter.Core.Entities;


public class StockMetric {
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public decimal PriceToEarnings { get; set; }
    public decimal DebtToEquity { get; set; }
    public decimal ReturnOnEquity { get; set; }
    public decimal DividendYield { get; set; }
    public bool PassesFilter { get; set; }
}