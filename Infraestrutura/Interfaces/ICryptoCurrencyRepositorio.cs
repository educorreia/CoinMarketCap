using Dominio;
using Infraestrutura.Repositorio;

namespace Infraestrutura.Interfaces
{
    public interface ICryptoCurrencyRepositorio
    {
        Task SaveCryptocurrencies(List<CryptoCurrency> cryptosCurrency);
        Task<List<CryptoCurrency>> GetCryptoCurrency();
        Task<List<CryptoCurrency>> GetCryptoCurrencyNotImport();
    }
}
