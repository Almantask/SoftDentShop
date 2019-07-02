using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor.Exceptions
{
    public class StockReportingAlreadyStartedException: Exception
    {
        public StockReportingAlreadyStartedException(): base("Stock reporting has already be started. Please stop it before starting again.")
        { }
    }
}
