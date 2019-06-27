using SoftDentShop.Domain.Models.StockRates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class StockRatesReporter : IStockRatesReporter
    {
        private readonly IStocksMonitor _stockMonitor;
        private readonly StockTimeScale _timeScale;

        public StockRatesReporter(StockTimeScale timeScale)
        {
            _stockMonitor = new StocksMonitor();
            _timeScale = timeScale;
        }

        public async Task<IList<StockRatesFull>> GetStockRatesAsync(IEnumerable<string> stockCodes)
        {
            var stockRatesFull = new List<StockRatesFull>();
            foreach (var code in stockCodes)
            {
                var singleStockRatesFull = await _stockMonitor.GetStockInfo(code, _timeScale);
                stockRatesFull.Add(singleStockRatesFull);
            }

            return stockRatesFull;
        }
    }
}
