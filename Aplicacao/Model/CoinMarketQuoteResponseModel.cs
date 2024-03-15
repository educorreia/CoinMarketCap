namespace Aplicacao.Model
{
    public class CoinMarketQuoteResponseModel
    {
        public CoinMarketQuoteData Data { get; set; }
    }

    public class CoinMarketQuoteData
    {
        public List<CoinMarketQuotes> Quotes { get; set; }
    }

    public class CoinMarketQuotes
    {
        public DateTime Timestamp { get; set; }
        public CoinMarketQuote Quote { get; set; }
    }

    public class CoinMarketQuote
    {
        public CoinMarketQuoteUSD USD { get; set; }
    }

    public class CoinMarketQuoteUSD
    {
        public double? Price { get; set; }
        public double? Market_Cap { get; set; }
        public double? Percent_Change_1h { get; set; }
        public double? Percent_Change_24h { get; set; }
        public double? Percent_Change_7d { get; set; }
        public double? Percent_Change_30d { get; set; }
        public double? Volume_24h { get; set; }
        public double? Total_supply { get; set; }
        public double? Circulating_supply { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
