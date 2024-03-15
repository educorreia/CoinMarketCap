using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Model
{
    public class CryptoCurrencyPeriodModel
    {
        public int Crypto { get; set; }
        public double Count { get; set; }
        public int Week { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
    }
}
