using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SoftDentShop.Domain.Models.StockRates;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public interface IStockRatesReporter
    {
        Task<IList<StockRateFull>> GetStockRatesAsync(IEnumerable<string> stockCodes, CancellationToken token);
    }
}