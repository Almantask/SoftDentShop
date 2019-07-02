using System;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class OnStockRequestedEventArgs: EventArgs
    {
        public string StockCode { get; }

        public OnStockRequestedEventArgs(string stock)
        {
            StockCode = stock;
        }
    }
}