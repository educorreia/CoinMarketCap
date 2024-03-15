using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Model
{
    public class CryptoCurrencyResponseModel
    {
        public List<CryptoCurrencyModel> Data { get; set; }
    }

    public class CryptoCurrencyModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
