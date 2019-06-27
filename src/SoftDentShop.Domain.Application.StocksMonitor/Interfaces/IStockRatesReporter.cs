using System.Collections.Generic;
using System.Threading.Tasks;
using SoftDentShop.Domain.Models.StockRates;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public interface IStockRatesReporter
    {
        Task<IList<StockRatesFull>> GetStockRatesAsync(IEnumerable<string> stockCodes);
    }
}