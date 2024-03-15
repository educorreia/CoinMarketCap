using Aplicacao.Interface;
using Aplicacao.Model;
using Dominio;
using Dominio.Entidades;
using Infraestrutura.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Aplicacao.Servico
{
    public class CryptoCurrencyServico : ICryptoCurrencyServico
    {
        private readonly ICryptoCurrencyRepositorio _cryptoCurrencyRepositorio;
        private readonly ICryptoCurrencyHistoricalRepositorio _currencyHistoricalRepository;
        private readonly IWeekCryptoCurrencyRepositorio _weekCryptoCurrencyRepositorio;
        private readonly HttpClient _httpClient;

        public CryptoCurrencyServico(
            ICryptoCurrencyRepositorio cryptoCurrencyRepositorio, 
            System.Net.Http.IHttpClientFactory httpClientFactory, 
            ICryptoCurrencyHistoricalRepositorio currencyHistoricalRepositorio,
            IWeekCryptoCurrencyRepositorio weekCryptoCurrencyRepositorio)
        {
            _cryptoCurrencyRepositorio = cryptoCurrencyRepositorio;
            _currencyHistoricalRepository = currencyHistoricalRepositorio;
            _weekCryptoCurrencyRepositorio = weekCryptoCurrencyRepositorio;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task RetrieveAndSaveCryptocurrencies()
        {
            var start = 1;
            int itemsPerPage = 5000;
            for (int page = 1; page <= 10; page++)
            {
                var cryptocurrenciesList = new List<CryptoCurrency>();
                CryptoCurrencyResponseModel response = await FetchCryptocurrencyData(start, itemsPerPage);
                foreach (var cryptocurrencyData in response.Data)
                {
                    cryptocurrenciesList.Add(new CryptoCurrency
                    {
                        Id = cryptocurrencyData.Id,
                        Name = cryptocurrencyData.Name,
                        Symbol = cryptocurrencyData.Symbol
                    });
                }
                if (cryptocurrenciesList.Any())
                    await _cryptoCurrencyRepositorio.SaveCryptocurrencies(cryptocurrenciesList);
                start += itemsPerPage;
            }
            
        }

        public async Task<List<CoinMarketCapModel>> FetchRecentUpdates()
        {
            var updatedCoinsList = new List<CoinMarketCapModel>();

            CoinMarketCapResponseModel initialResponse = await FetchLatestData(start: 1, limit: 1);

            if (initialResponse != null)
            {
                var start = 1;
                var itemsPerPage = 5000;
                var pageCount = Math.Ceiling(initialResponse.Status.Total_Count / 1000.00);

                for (int i = 1; i <= pageCount; i++)
                {
                    CoinMarketCapResponseModel pageResponse = await FetchLatestData(start: start, itemsPerPage);

                    foreach (var item in pageResponse.Data)
                    {
                        updatedCoinsList.Add(new CoinMarketCapModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Symbol = item.Symbol,
                            Percent_Change_7d = item.Quote.USD.Percent_Change_7d,
                            Price = item.Quote.USD.Price,
                            Market_Cap = item.Quote.USD.Market_Cap,
                            Volume_24h = item.Quote.USD.Volume_24h
                        });
                    }
                    start += itemsPerPage;
                }
            }
            return updatedCoinsList
                    .Where(n => n.Market_Cap > 1000000 && n.Volume_24h > 100000000)
                    .OrderByDescending(n => n.Market_Cap)
                    .Skip(50)
                    .OrderByDescending(n => n.Market_Cap)
                    .Take(150)
                    .OrderByDescending(n => n.Percent_Change_7d).Take(20).ToList();
        }

        public async Task<List<CoinMarketCapModel>> FetchAndSaveRecentUpdates()
        {
            var updatedCoinsList = await FetchRecentUpdates();

            await _weekCryptoCurrencyRepositorio.SaveWeekCryptoCurrency(updatedCoinsList.Select(n => new WeekCryptoCurrency
            {
                CryptoCurrencyId = n.Id,
                MarketCap = n.Market_Cap.GetValueOrDefault(),
                Percent24h = n.Percent_Change_24h.GetValueOrDefault(),
                Percent30d = n.Percent_Change_30d.GetValueOrDefault(),
                Percent7d = n.Percent_Change_7d.GetValueOrDefault(),
                Price = n.Price.GetValueOrDefault(),
                Week = 1
            }).ToList());

            return updatedCoinsList;
        }

        public async Task RetrieveAndSaveHistoricalData(DateTime startDate, DateTime endDate)
        {
            for (DateTime currentData = startDate; currentData <= endDate; currentData = currentData.AddDays(1))
            {
                var start = 1;
                var itemsPerPage = 5000;
                var historicalDataList = new List<CryptoCurrencyHistorical>();

                for (int block = 1; block <= 3; block++)
                {
                    CoinMarketCapResponseModel response = await FetchHistoricalData(start: start, itemsPerPage, currentData);
                    foreach (var item in response.Data)
                    {
                        historicalDataList.Add(new CryptoCurrencyHistorical
                        {
                            CryptoId = item.Id,
                            Price = item.Quote.USD.Price,
                            Volume24h = item.Quote.USD.Volume_24h,
                            PercentChange1h = item.Quote.USD.Percent_Change_1h,
                            PercentChange24h = item.Quote.USD.Percent_Change_24h,
                            PercentChange7d = item.Quote.USD.Percent_Change_7d,
                            PercentChange30d = item.Quote.USD.Percent_Change_30d,
                            MarketCap = item.Quote.USD.Market_Cap,
                            TotalSupply = item.Quote.USD.Market_Cap_Dominance,
                            CirculatingSupply = item.Quote.USD.Fully_Diluted_Market_Cap,
                            //LastUpdated = item.Quote.USD.Last_Updated,
                            Timestamp = currentData
                        });
                    }
                    start += itemsPerPage;
                }
                if (historicalDataList.Any())
                    await _currencyHistoricalRepository.SaveHistoricalData(historicalDataList);
            }
        }

        public async Task CreateHistoricalData()
        {
            var cryptos = await _cryptoCurrencyRepositorio.GetCryptoCurrencyNotImport();

            int daysPassed = GetDaysPassedInYearUntilToday(DateTime.Now);
            foreach (var crypto in cryptos)
            {
                await CreateHistoricalDataForCrypto(crypto.Id, daysPassed);
            }
        }

        public async Task<double> Simulator()
        {
            DateTime date = new DateTime(2024, 1, 4);
            double capital = 300.00;
            double countCryptocurrency = 15.00;
            double stop = 10.00;
            DateTime stopDate = new DateTime(2024, 3, 11);
            int interval = 7;
            int week = 1;

            var cryptoCurrencyPeriodList = new List<CryptoCurrencyPeriodModel>();

            while (capital > stop && date < stopDate)
            {
                
                var response = await getCrytocurrencies(date);
                if(response.Count() > 0)
                {
                    var count = response.Count() < countCryptocurrency ? response.Count() : countCryptocurrency;
                    double apportionment = capital / count;
                    foreach (var item in response)
                    {
                        cryptoCurrencyPeriodList.Add(new CryptoCurrencyPeriodModel
                        {
                            Crypto = item.CryptoId,
                            Count = apportionment / item.Price.Value,
                            PurchasePrice = item.Price.Value,
                            Week = week
                        });
                    }
                    date = date.AddDays(interval);
                    if(date > stopDate)
                    {
                        double capitalTemp = 0;
                        foreach (var item in cryptoCurrencyPeriodList)
                            capitalTemp += item.Count * item.PurchasePrice;
                        capital = capitalTemp;
                    }
                    else
                    {
                        capital = await calculateCapital(cryptoCurrencyPeriodList, date);
                    }
                }
                else
                {
                    date = date.AddDays(interval);
                }
                
                week++;
            }
            return capital;
        }

        private int GetDaysPassedInYearUntilToday(DateTime date)
        {
            DateTime startOfYear = new DateTime(date.Year, 1, 1);

            return (int)(date - startOfYear).TotalDays + 1;
        }

        private async Task CreateHistoricalDataForCrypto(int criptoId, int days)
        {
            var historicalDataList = new List<CryptoCurrencyHistorical>();
            CoinMarketQuoteResponseModel quoteResponse = await FetchHistoricalQuotes(criptoId, days);
            if(quoteResponse != null && quoteResponse.Data != null && quoteResponse.Data.Quotes.Any())
            {
                foreach (var quote in quoteResponse.Data.Quotes)
                {
                    historicalDataList.Add(new CryptoCurrencyHistorical
                    {
                        CryptoId = criptoId,
                        Price = quote.Quote.USD.Price,
                        Volume24h = quote.Quote.USD.Volume_24h,
                        PercentChange1h = quote.Quote.USD.Percent_Change_1h,
                        PercentChange24h = quote.Quote.USD.Percent_Change_24h,
                        PercentChange7d = quote.Quote.USD.Percent_Change_7d,
                        PercentChange30d = quote.Quote.USD.Percent_Change_30d,
                        MarketCap = quote.Quote.USD.Market_Cap,
                        CirculatingSupply = quote.Quote.USD.Circulating_supply,
                        TotalSupply = quote.Quote.USD.Total_supply,
                        Timestamp = quote.Quote.USD.Timestamp
                    });
                }
                if (historicalDataList.Any())
                    await _currencyHistoricalRepository.SaveHistoricalData(historicalDataList);
            }
            
        }

        private async Task<CoinMarketCapResponseModel> FetchLatestData(int start, int limit)
        {
            var baseUrl = "https://pro-api.coinmarketcap.com";
            var requestUrl = $"{baseUrl}/v1/cryptocurrency/listings/latest?start={start}&limit={limit}&sort=market_cap&cryptocurrency_type=all&tag=all";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("X-CMC_PRO_API_KEY", "8c50883c-0c86-4eaa-ad69-a25ec1799121");
            request.Headers.Add("X-Accept", "*/*");

            var response = await _httpClient.SendAsync(request);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinMarketCapResponseModel>(jsonResponse.ToString());
        }

        private async Task<CoinMarketCapResponseModel> FetchHistoricalData(int start, int limit, DateTime date)
        {
            var baseUrl = "https://pro-api.coinmarketcap.com";
            var requestUrl = $"{baseUrl}/v1/cryptocurrency/listings/historical?date={date.ToString("yyyy-MM-dd")}&start={start}&limit={limit}&sort=market_cap&cryptocurrency_type=all";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("X-CMC_PRO_API_KEY", "8c50883c-0c86-4eaa-ad69-a25ec1799121");
            request.Headers.Add("X-Accept", "*/*");

            var response = await _httpClient.SendAsync(request);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinMarketCapResponseModel>(jsonResponse.ToString());
        }

        private async Task<CryptoCurrencyResponseModel> FetchCryptocurrencyData(int start, int itemsPerPage)
        {
            var baseUrl = "https://pro-api.coinmarketcap.com";
            var endpoint = $"/v1/cryptocurrency/map?start={start}&limit={itemsPerPage}&sort=id";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{endpoint}");
            request.Headers.Add("X-CMC_PRO_API_KEY", "8c50883c-0c86-4eaa-ad69-a25ec1799121");
            request.Headers.Add("X-Accept", "*/*");

            var response = await _httpClient.SendAsync(request);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CryptoCurrencyResponseModel>(jsonResponse.ToString());
        }

        private async Task<CoinMarketQuoteResponseModel> FetchHistoricalQuotes(int id, int days)
        {
            var baseUrl = "https://pro-api.coinmarketcap.com";
            var requestUrl = $"{baseUrl}/v2/cryptocurrency/quotes/historical?id={id}&&count={days}&interval=daily";
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("X-CMC_PRO_API_KEY", "8c50883c-0c86-4eaa-ad69-a25ec1799121");
            request.Headers.Add("X-Accept", "*/*");

            var response = await _httpClient.SendAsync(request);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinMarketQuoteResponseModel>(jsonResponse.ToString());
        }

        private async Task<List<CryptoCurrencyHistorical>> getCrytocurrencies(DateTime date)
        {

            var updatedCoinsList = await _currencyHistoricalRepository.GetHistoricalData(date);

            return updatedCoinsList
                    .Where(n => n.MarketCap > 1000000 && n.Volume24h > 100000000)
                    .OrderByDescending(n => n.MarketCap)
                    .Skip(50)
                    .OrderByDescending(n => n.MarketCap)
                    .Take(150)
                    .OrderByDescending(n => n.PercentChange7d).Take(15).ToList();
        }

        private async Task<double> calculateCapital(List<CryptoCurrencyPeriodModel> list, DateTime date)
        {
            double capital = 0;
            foreach (var item in list)
            {
                var updatedCoins = (await _currencyHistoricalRepository.GetHistoricalData(date, item.Crypto)).FirstOrDefault();
                capital += item.Count * updatedCoins.Price.Value;

            }
            return capital;
        }
    }
}
