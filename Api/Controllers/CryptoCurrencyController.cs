using Aplicacao.Interface;
using Aplicacao.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly ICryptoCurrencyServico _cryptoCurrencyServico;

        public CryptoCurrencyController(ICryptoCurrencyServico cryptoCurrencyServico)
        {
            _cryptoCurrencyServico = cryptoCurrencyServico;
        }

        [HttpGet("last-update")]
        [AllowAnonymous]
        public async Task<IEnumerable<CoinMarketCapModel>> GetLastUpdate()
        {
            return await _cryptoCurrencyServico.FetchRecentUpdates();
        }

        [HttpPost("save-last-update")]
        [AllowAnonymous]
        public async Task<IEnumerable<CoinMarketCapModel>> GetAndSaveLastUpdate()
        {
            return await _cryptoCurrencyServico.FetchAndSaveRecentUpdates();
        }

        [HttpGet("crypto-currency")]
        [AllowAnonymous]
        public async Task GetCryptoCurrency()
        {
            await _cryptoCurrencyServico.RetrieveAndSaveCryptocurrencies();
        }

        [HttpPost("historical")]
        [AllowAnonymous]
        public async Task CreateHistoricalData()
        {
            await _cryptoCurrencyServico.CreateHistoricalData();
        }

        [HttpPost("simulator")]
        [AllowAnonymous]
        public async Task<double> RunSimulator()
        {
            return await _cryptoCurrencyServico.Simulator();
        }
    }
}