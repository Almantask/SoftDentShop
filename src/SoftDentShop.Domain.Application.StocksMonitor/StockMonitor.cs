using Newtonsoft.Json;
using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
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

        public async Task<StockRateFull> GetStockInfoAsync(string stockCode, StockTimeScale timeScale, CancellationToken token)
        {
            try
            {
                StockRequested?.Invoke(this, new OnStockRequestedEventArgs(stockCode));

                var endpoint = AdjustEndpoint(stockCode, timeScale);
                var response = await _client.GetAsync(endpoint, token);

                StockRateFull stocks = new StockRateFull();
                if (response.IsSuccessStatusCode)
                {
                    // What does content contain initially if not string?
                    var json = await response.Content.ReadAsStringAsync();
                    stocks = JsonConvert.DeserializeObject<StockRateFull>(json);
                    if (stocks.MetaData == null)
                    {
                        throw new HttpRequestException(json);
                    }
                }
                else
                {
                    throw new HttpRequestException($"Server returned {response.StatusCode}");
                }

                StockRetrieved?.Invoke(this, new OnStockRetrievedEventArgs(stocks));
                return stocks;

            }
            catch (HttpRequestException e)
            {
                throw e;
            }

        }

        private string AdjustEndpoint(string stockCode, StockTimeScale timeScale)
        {
            const string endpoint = @"https://www.alphavantage.co/query?function={0}&symbol={1}&apikey={2}";
            const string endpointMinutes = @"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={0}&interval={1}min&outputsize=full&apikey={2}";

            var adjustedEndpoint = "";
            if (timeScale == StockTimeScale.Hourly)
            {
                adjustedEndpoint = string.Format(endpointMinutes, stockCode, 60, APIKey);
            }
            else if (timeScale == StockTimeScale.Minutely)
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
