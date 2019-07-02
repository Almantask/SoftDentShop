using SoftDentShop.Domain.Models.StockRates;
using System;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
{
    public class OnStockRetrievedEventArgs : EventArgs
    {
        public StockRateFull StockRateFull { get; }

        public OnStockRetrievedEventArgs(StockRateFull stockRateFull)
        {
            StockRateFull = stockRateFull;
        }
    }
}
