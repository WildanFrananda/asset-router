namespace AssetRouter.Infrastructure.DataSources;

using System.Text.Json;
using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;

public class HttpStockDataSource : IStockDataSource {
    private readonly HttpClient _http;
    private static readonly string[] IdxTickers = new[] {
        "BBCA.JK", "BBRI.JK", "BMRI.JK", "TLKM.JK", "ASII.JK",
        "ICBP.JK", "UNVR.JK", "ADRO.JK", "AMRT.JK", "GOTO.JK"
    };

    public HttpStockDataSource(HttpClient http) {
        _http = http;
    }

    public async Task<List<StockMetric>> GetStocksAsync() {
        var stocks = new List<StockMetric>();

        foreach (var tickerSymbol in IdxTickers) {
            try {
                var url = $"https://query1.finance.yahoo.com/v8/finance/chart/{tickerSymbol}";
                using var response = await _http.GetAsync(url);

                if (response.IsSuccessStatusCode) {
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var doc = await JsonDocument.ParseAsync(stream);
                    var resultElement = doc.RootElement
                        .GetProperty("chart")
                        .GetProperty("result")[0]
                        .GetProperty("meta");

                    var rawSymbol = resultElement.GetProperty("symbol").GetString() ?? tickerSymbol;
                    var cleanTicker = rawSymbol.Replace(".JK", "");
                    var longName = resultElement.TryGetProperty("longName", out var nameProp)
                        ? nameProp.GetString() ?? cleanTicker
                        : cleanTicker;

                    stocks.Add(new StockMetric {
                        Ticker = cleanTicker,
                        Name = longName,
                        Sector = GetSectorForTicker(cleanTicker),
                        PriceToEarnings = GetEstimatedPer(cleanTicker),
                        DebtToEquity = GetEstimatedDer(cleanTicker),
                        ReturnOnEquity = GetEstimatedRoe(cleanTicker),
                        DividendYield = GetEstimatedDivYield(cleanTicker)
                    });
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error fetching {tickerSymbol} from Yahoo Finance: {ex.Message}");
            }
        }

        if (stocks.Count == 0) {
            stocks = GetFallbackStocks();
        }

        return stocks;
    }

    private static string GetSectorForTicker(string ticker) => ticker switch {
        "BBCA" or "BBRI" or "BMRI" => "Financials",
        "TLKM" => "Telecommunication",
        "ASII" => "Automotive & Industrial",
        "ICBP" or "UNVR" => "Consumer Goods",
        "ADRO" => "Energy & Mining",
        "AMRT" => "Retail",
        "GOTO" => "Technology",
        _ => "General"
    };

    private static decimal GetEstimatedPer(string ticker) => ticker switch {
        "BBCA" => 22.4m,
        "BBRI" => 13.8m,
        "BMRI" => 11.6m,
        "TLKM" => 14.2m,
        "ASII" => 6.8m,
        "ICBP" => 15.4m,
        "UNVR" => 24.1m,
        "ADRO" => 4.9m,
        "AMRT" => 34.2m,
        "GOTO" => -8.5m,
        _ => 15.0m
    };

    private static decimal GetEstimatedDer(string ticker) => ticker switch {
        "BBCA" => 0.85m,
        "BBRI" => 0.92m,
        "BMRI" => 0.88m,
        "TLKM" => 0.72m,
        "ASII" => 1.02m,
        "ICBP" => 0.85m,
        "UNVR" => 2.15m,
        "ADRO" => 0.38m,
        "AMRT" => 0.65m,
        "GOTO" => 0.15m,
        _ => 0.8m
    };

    private static decimal GetEstimatedRoe(string ticker) => ticker switch {
        "BBCA" => 21.5m,
        "BBRI" => 19.2m,
        "BMRI" => 20.8m,
        "TLKM" => 18.4m,
        "ASII" => 15.6m,
        "ICBP" => 22.1m,
        "UNVR" => 62.4m,
        "ADRO" => 24.8m,
        "AMRT" => 28.3m,
        "GOTO" => -12.4m,
        _ => 16.0m
    };

    private static decimal GetEstimatedDivYield(string ticker) => ticker switch {
        "BBCA" => 2.4m,
        "BBRI" => 4.1m,
        "BMRI" => 4.8m,
        "TLKM" => 4.5m,
        "ASII" => 8.2m,
        "ICBP" => 3.1m,
        "UNVR" => 4.6m,
        "ADRO" => 14.5m,
        "AMRT" => 1.2m,
        "GOTO" => 0.0m,
        _ => 3.0m
    };

    private static List<StockMetric> GetFallbackStocks() {
        return new List<StockMetric> {
            new StockMetric { Ticker = "BBCA", Name = "Bank Central Asia Tbk", Sector = "Financials", PriceToEarnings = 22.4m, DebtToEquity = 0.85m, ReturnOnEquity = 21.5m, DividendYield = 2.4m },
            new StockMetric { Ticker = "BBRI", Name = "Bank Rakyat Indonesia Tbk", Sector = "Financials", PriceToEarnings = 13.8m, DebtToEquity = 0.92m, ReturnOnEquity = 19.2m, DividendYield = 4.1m },
            new StockMetric { Ticker = "BMRI", Name = "Bank Mandiri Tbk", Sector = "Financials", PriceToEarnings = 11.6m, DebtToEquity = 0.88m, ReturnOnEquity = 20.8m, DividendYield = 4.8m },
            new StockMetric { Ticker = "TLKM", Name = "Telkom Indonesia Tbk", Sector = "Telecommunication", PriceToEarnings = 14.2m, DebtToEquity = 0.72m, ReturnOnEquity = 18.4m, DividendYield = 4.5m },
            new StockMetric { Ticker = "ASII", Name = "Astra International Tbk", Sector = "Automotive & Industrial", PriceToEarnings = 6.8m, DebtToEquity = 1.02m, ReturnOnEquity = 15.6m, DividendYield = 8.2m }
        };
    }
}