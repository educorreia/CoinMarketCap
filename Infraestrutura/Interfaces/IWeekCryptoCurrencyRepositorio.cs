using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Interfaces
{
    public interface IWeekCryptoCurrencyRepositorio
    {
        Task SaveWeekCryptoCurrency(List<WeekCryptoCurrency> weekCryptoCurrency);
    }
}
