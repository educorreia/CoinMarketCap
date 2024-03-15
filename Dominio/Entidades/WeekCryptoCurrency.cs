namespace Dominio
{
    public class WeekCryptoCurrency
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public double Price { get; set; }
        public double MarketCap { get; set; }
        public double Percent24h { get; set; }
        public double Percent7d { get; set; }
        public double Percent30d { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public DateTime PurchaseDay { get; set; }
        public DateTime SaleDay { get; set; }
        public int CryptoCurrencyId { get; set; }
        public CryptoCurrency CryptoCurrency { get; set; }
    }
}
