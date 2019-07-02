using SoftDentShop.Domain.Models.StockRates;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class StockRatesReporter : IStockRatesReporter
    {
        private readonly IStocksMonitor _stockMonitor;
        private readonly StockTimeScale _timeScale;

        public StockRatesReporter(StockTimeScale timeScale, IStocksMonitor monitor)
        {
            _stockMonitor = monitor;
            _timeScale = timeScale;
        }

        public async Task<IList<StockRateFull>> GetStockRatesAsync(IEnumerable<string> stockCodes, CancellationToken token)
        {
            var stockRatesFull = new List<StockRateFull>();
            foreach (var code in stockCodes)
            {
                var singleStockRatesFull = await _stockMonitor.GetStockInfoAsync(code, _timeScale, token);
                if (singleStockRatesFull == null) continue;
                stockRatesFull.Add(singleStockRatesFull);
            }

            return stockRatesFull;
        }
    }
}
