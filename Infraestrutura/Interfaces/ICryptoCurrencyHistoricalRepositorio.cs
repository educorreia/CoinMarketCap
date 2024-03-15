using Dominio.Entidades;
using Infraestrutura.Repositorio;

namespace Infraestrutura.Interfaces
{
    public interface ICryptoCurrencyHistoricalRepositorio
    {
        Task SaveHistoricalData(List<CryptoCurrencyHistorical> historicos);
        Task<List<CryptoCurrencyHistorical>> GetHistoricalData(DateTime date, int? cryptoId = null);
    }
}
