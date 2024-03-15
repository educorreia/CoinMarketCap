namespace Dominio.Entidades
{
    public class CryptoCurrencyHistorical
    {
        public int Id { get; set; }
        public int CryptoId { get; set; }
        public double? Price { get; set; }
        public double? Volume24h { get; set; }
        public double? PercentChange1h { get; set; }
        public double? PercentChange24h { get; set; }
        public double? PercentChange7d { get; set; }
        public double? PercentChange30d { get; set; }
        public double? MarketCap { get; set; }
        public double? TotalSupply { get; set; }
        public double? CirculatingSupply { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
