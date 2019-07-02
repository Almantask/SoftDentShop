using System;
using System.Threading;
using System.Threading.Tasks;
using SoftDentShop.Domain.Models.StockRates;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
{
    public interface IStocksMonitor
    {
        event EventHandler<OnStockRequestedEventArgs> StockRequested;
        event EventHandler<OnStockRetrievedEventArgs> StockRetrieved;

        Task<StockRateFull> GetStockInfoAsync(string stockCode, StockTimeScale timeScale, CancellationToken token);
    }
}