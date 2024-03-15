namespace Aplicacao.Model
{
    public class CoinMarketCapResponseModel
    {
        public CoinMarketCapStatus Status { get; set; }
        public List<CoinMarketCapData> Data { get; set; }
    }

    public class CoinMarketCapStatus
    {
        public int Total_Count { get; set; }
    }

    public class CoinMarketCapData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public CoinMarketCapQuote Quote { get; set; }
    }

    public class CoinMarketCapQuote
    {
        public CoinMarketCapUSD USD { get; set; }
    }

    public class CoinMarketCapUSD
    {
        public double? Price { get; set; }
        public double? Market_Cap { get; set; }
        public double? Percent_Change_1h { get; set; }
        public double? Percent_Change_24h { get; set; }
        public double? Percent_Change_7d { get; set; }
        public double? Percent_Change_30d { get; set; }
        public double? Volume_24h { get; set; }
        public double? Volume_Change_24h { get; set; }
        public double? Market_Cap_Dominance { get; set; }
        public double? Fully_Diluted_Market_Cap { get; set; }
        public DateTime Last_Updated { get; set; }
    }
}
