using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.StockRates
{
    public class NoStockRatesMetadata:StockRatesMetadata
    {
        public NoStockRatesMetadata()
        {
            Description = "No metadata found";
            StockCode = "N/A";
            LastRefreshed = DateTime.Now;
            TimeZone = "N/A";
        }
    }
}
