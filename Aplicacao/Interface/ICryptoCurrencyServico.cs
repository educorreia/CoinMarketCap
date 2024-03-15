using Aplicacao.Model;

namespace Aplicacao.Interface
{
    public interface ICryptoCurrencyServico
    {
        Task<List<CoinMarketCapModel>> FetchRecentUpdates();
        Task RetrieveAndSaveHistoricalData(DateTime startDate, DateTime endDate);
        Task RetrieveAndSaveCryptocurrencies();
        Task CreateHistoricalData();
        Task<double> Simulator();
        Task<List<CoinMarketCapModel>> FetchAndSaveRecentUpdates();
    }
}
