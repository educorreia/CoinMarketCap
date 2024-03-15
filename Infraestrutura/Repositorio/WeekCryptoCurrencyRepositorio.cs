using Dominio;
using Infraestrutura.DBContext;
using Infraestrutura.Interfaces;

namespace Infraestrutura.Repositorio
{
    public class WeekCryptoCurrencyRepositorio : IWeekCryptoCurrencyRepositorio
    {
        private readonly AppDbContext _contexto;
        public WeekCryptoCurrencyRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task SaveWeekCryptoCurrency(List<WeekCryptoCurrency> weekCryptoCurrency)
        {
            await _contexto.AddRangeAsync(weekCryptoCurrency);
            await _contexto.SaveChangesAsync();
        }
    }
}
