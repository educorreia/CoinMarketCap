using Dominio;
using Dominio.Entidades;
using Infraestrutura.DBContext;
using Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio
{
    public class CryptoCurrencyRepositorio : ICryptoCurrencyRepositorio
    {
        private readonly AppDbContext _contexto;
        public CryptoCurrencyRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task SaveCryptocurrencies(List<CryptoCurrency> cryptosCurrency)
        {
            await _contexto.AddRangeAsync(cryptosCurrency);
            await _contexto.SaveChangesAsync();
        }

        public async Task<List<CryptoCurrency>> GetCryptoCurrency()
        {
            return await _contexto.CryptoCurrency.OrderBy(n => n.Id).ToListAsync();
        }

        public async Task<List<CryptoCurrency>> GetCryptoCurrencyNotImport()
        {
            return await _contexto.CryptoCurrency
                                    .Where(n => !_contexto.CryptoCurrencyHistorical.Select(n => n.CryptoId).Distinct().ToList().Contains(n.Id))
                                    .OrderBy(n => n.Id).ToListAsync();
        }
    }
}
