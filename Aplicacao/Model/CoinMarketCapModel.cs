namespace Aplicacao.Model
{
    public class CoinMarketCapModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double? Price { get; set; }
        public double? Market_Cap { get; set; }
        public double? Percent_Change_24h { get; set; }
        public double? Percent_Change_7d { get; set; }
        public double? Percent_Change_30d { get; set; }
        public double? Volume_24h { get; set; }
    }
}
