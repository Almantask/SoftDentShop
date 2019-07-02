using SoftDentShop.Domain.ApplicationCore.StockMonitor.Exceptions;
using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
{
    public class StocksMonitoringRoutine : IStocksMonitoringRoutine
    {
        public event EventHandler<OnStockReportStartedEventArgs> ReportingStarted;
        public event EventHandler<OnStockReportEndedEventArgs> ReportingEnded;

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
                {StockTimeScale.Monthly, 3},
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

        public async Task Start()
        {
            ReportingStarted?.Invoke(this, new OnStockReportStartedEventArgs(DateTime.Now));
            if(_cancellationToken != null)
            {
                throw new StockReportingAlreadyStartedException();
            }
            _cancellationToken = new CancellationTokenSource();
            await PeriodicStockUpdateAsync(new TimeSpan(0, 0, 0, _timeScaleMapping[_timeScale] / 30), _cancellationToken);
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
            ReportingEnded?.Invoke(this, new OnStockReportEndedEventArgs(_stockRatesFullHistory));
        }

        public async Task PeriodicStockUpdateAsync(TimeSpan interval, CancellationTokenSource cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();

                await LoadStockAsync();
                await Task.Delay(interval);
            };
        }

        private async Task LoadStockAsync()
        {
            var stockRatesFull = await _reporter.GetStockRatesAsync(_stockSymbols, _cancellationToken.Token);
            foreach (var stockRateFull in stockRatesFull)
            {
                AddToStockHistory(stockRateFull);
            }
        }

        private void AddToStockHistory(StockRateFull stockRateFull)
        {
            var stockSymbol = stockRateFull.MetaData.StockCode;
            var stockRates = stockRateFull.StockRates;
            if (_stockRatesFullHistory.ContainsKey(stockSymbol))
            {
                //foreach (var stockRate in stockRates)
                //{
                //    _stockRatesFullHistory[stockSymbol].Add(stockRate.Key, stockRate.Value);
                //}
            }
            else
            {
                _stockRatesFullHistory.Add(stockSymbol, stockRates);
            }
        }
    }
}
