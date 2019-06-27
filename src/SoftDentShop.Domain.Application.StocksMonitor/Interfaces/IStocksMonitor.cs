using System;
using System.Threading.Tasks;
using SoftDentShop.Domain.Models.StockRates;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public interface IStocksMonitor
    {
        event EventHandler<OnStockRequestedEventArgs> StockRequested;
        event EventHandler<OnStockRetrievedEventArgs> StockRetrieved;

        Task<StockRatesFull> GetStockInfo(string stockCode, StockTimeScale timeScale);
    }
}