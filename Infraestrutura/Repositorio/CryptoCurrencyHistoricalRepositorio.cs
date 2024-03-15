using Dominio.Entidades;
using Infraestrutura.DBContext;
using Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class CryptoCurrencyHistoricalRepositorio : ICryptoCurrencyHistoricalRepositorio
    {
        private readonly AppDbContext _contexto;
        public CryptoCurrencyHistoricalRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task SaveHistoricalData(List<CryptoCurrencyHistorical> historicos)
        {
            await _contexto.AddRangeAsync(historicos);
            await _contexto.SaveChangesAsync();
        }

        public async Task<List<CryptoCurrencyHistorical>> GetHistoricalData(DateTime date, int? cryptoId = null)
        {
            var query = _contexto.CryptoCurrencyHistorical.Where(historical => historical.Timestamp == date);

            if (cryptoId.HasValue)
                query = query.Where(n => n.CryptoId == cryptoId);

            return query.ToList();
        }
    }
}
