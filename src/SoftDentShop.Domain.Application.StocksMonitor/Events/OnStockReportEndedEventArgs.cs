using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public class OnStockReportEndedEventArgs: EventArgs
    {
        public IDictionary<string, Dictionary<DateTime, StockRate>> StockRatesHistory { get; }

        public OnStockReportEndedEventArgs(IDictionary<string, Dictionary<DateTime, StockRate>> history)
        {
            StockRatesHistory = history;
        }
    }
}