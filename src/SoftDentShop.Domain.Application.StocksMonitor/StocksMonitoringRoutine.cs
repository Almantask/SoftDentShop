using SoftDentShop.Domain.Application.StockMonitor.Exceptions;
using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class StocksMonitoringRoutine : IStocksMonitoringRoutine
    {
        public event EventHandler<OnStockReportStartedEventArgs> ReportingStarted;
        public event EventHandler<OnStockReportEndedEventArgs> ReportingEnded;

        private Task _ongoingStockReport;
        private IDictionary<string, Dictionary<DateTime, StockRate>> _stockRatesFullHistory;

        private readonly IStocksMonitor _monitor;
        private readonly IStockRatesReporter _reporter;

        private readonly StockTimeScale _timeScale;
        /// <summary>
        /// Time scale in ms.
        /// </summary>
        private readonly static IDictionary<StockTimeScale, int> _timeScaleMapping;
        private CancellationTokenSource _cancellationToken;
        
        private IEnumerable<string> _stockSymbols;

        static StocksMonitoringRoutine()
        {
            _timeScaleMapping = new Dictionary<StockTimeScale, int>
            {
                {StockTimeScale.Minutely, 60},
                {StockTimeScale.Hourly, 60 * 60},
                {StockTimeScale.Daily, 24 * 60 * 60},
                {StockTimeScale.Weekly, 7 * 24 * 60 * 60},
                {StockTimeScale.Monthly, 4 * 7 * 24 * 3600},
            };
        }

        public StocksMonitoringRoutine(IStockRatesReporter reporter, IStocksMonitor monitor, StockTimeScale timeScale, params string[] stockSymbols)
        {
            _stockRatesFullHistory = new Dictionary<string, Dictionary<DateTime, StockRate>>();
            _monitor = monitor;
            _reporter = reporter;
            _stockSymbols = stockSymbols;
            _timeScale = timeScale;
        }

        public void Start()
        {
            if(_cancellationToken != null)
            {
                throw new StockReportingAlreadyStartedException();
            }
            _cancellationToken = new CancellationTokenSource();
            _ongoingStockReport = PeriodicStockUpdateAsync(new TimeSpan(0, 0, 1, 0, 0), _cancellationToken);
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
        }

        public async Task PeriodicStockUpdateAsync(TimeSpan interval, CancellationTokenSource cancellationToken)
        {
            while (true)
            {
                await UpdateStocksAsync();
                await Task.Delay(interval, cancellationToken.Token);
            };
        }

        private async Task UpdateStocksAsync()
        {
            var stockRatesFull = await _reporter.GetStockRatesAsync(_stockSymbols);
            foreach (var stockRateFull in stockRatesFull)
            {
                UpdateHistory(stockRateFull);
            }
        }

        private void UpdateHistory(StockRatesFull stockRateFull)
        {
            var stockSymbol = stockRateFull.MetaData.StockCode;
            var stockRates = stockRateFull.StockRates;
            if (_stockRatesFullHistory.ContainsKey(stockSymbol))
            {
                foreach (var stockRate in stockRates)
                {
                    _stockRatesFullHistory[stockSymbol].Add(stockRate.Key, stockRate.Value);
                }
            }
            else
            {
                foreach (var stockRate in stockRates)
                {
                    _stockRatesFullHistory.Add(stockSymbol, stockRates);
                    _stockRatesFullHistory[stockSymbol].Add(stockRate.Key, stockRate.Value);
                }
            }
        }
    }
}
