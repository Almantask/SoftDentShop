using Newtonsoft.Json;
using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class StocksMonitor : IStocksMonitor
    {
        public event EventHandler<OnStockRequestedEventArgs> StockRequested;
        public event EventHandler<OnStockRetrievedEventArgs> StockRetrieved;

        private const string APIKey = "3TEIM6A4WCZIRZR0";

        private static Dictionary<StockTimeScale, string> TimeScaleMap;
        private static HttpClient _client;
        
        static StocksMonitor()
        {
            TimeScaleMap = new Dictionary<StockTimeScale, string>
            {
                {StockTimeScale.Hourly, "TIME_SERIES_INTRADAY"},
                {StockTimeScale.Daily, "TIME_SERIES_DAILY"},
                {StockTimeScale.Weekly, "TIME_SERIES_WEEKLY"},
                {StockTimeScale.Monthly, "TIME_SERIES_MONTHLY"}
            };

            _client = new HttpClient();
        }

        public async Task<StockRatesFull> GetStockInfo(string stockCode, StockTimeScale timeScale)
        {
            var endpoint = AdjustEndpoint(stockCode, timeScale);
            var response = await _client.GetAsync(endpoint);

            StockRatesFull stocks = new StockRatesFull();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                stocks = JsonConvert.DeserializeObject<StockRatesFull>(json);
            }
            return stocks;
        }

        private string AdjustEndpoint(string stockCode, StockTimeScale timeScale)
        {
            const string endpoint = @"https://www.alphavantage.co/query?function={0}&symbol={1}&apikey={2}";
            const string endpointMinutes = @"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={0}&interval={1}min&outputsize=full&apikey={2}";

            var adjustedEndpoint = "";
            if(timeScale == StockTimeScale.Hourly)
            {
                adjustedEndpoint = string.Format(endpointMinutes, stockCode, 60, APIKey);
            }
            else if(timeScale == StockTimeScale.Minutely)
            {
                adjustedEndpoint = string.Format(endpointMinutes, stockCode, 1, APIKey);
            }
            else
            {
                var timeFunction = TimeScaleMap[timeScale];
                adjustedEndpoint = string.Format(endpoint, timeFunction, stockCode, APIKey);
            }

            return adjustedEndpoint;
        }
    }
}
