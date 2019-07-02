using System;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
{
    public class OnStockReportStartedEventArgs: EventArgs
    {
        public DateTime TimeStarted { get; }

        public OnStockReportStartedEventArgs(DateTime started)
        {
            TimeStarted = started;
        }
    }
}