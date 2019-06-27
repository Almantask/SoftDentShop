using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor.Tests
{
    [TestFixture]
    public class StocksMonitorTests
    {
        [Test]
        public async Task GetStocks_Monthly_Ok()
        {
            var client = new StocksMonitor();
            // MCFT- microsoft
            var stocks = await client.GetStockInfo("MCK", StockTimeScale.Monthly);
            Assert.IsNotNull(stocks);
        }
    }
}
