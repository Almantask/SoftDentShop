using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
            var result = await client.GetStockInfoAsync("MSFT", StockTimeScale.Monthly);
            Assert.IsNotNull(result.StockRates);
        }

        private async void Client_StockRequested(object sender, 
            OnStockRequestedEventArgs e)
        {
            // UI + One more thread to move actions to thraed pool.
            await Task.Run(() => Thread.Sleep(5000)).ConfigureAwait(false);
            await Task.Run(() => Thread.Sleep(5000)).ConfigureAwait(false);
            await Task.Run(() => Thread.Sleep(5000)).ConfigureAwait(false);
        }

        private async void Client_StockRequested1(object sender,
    OnStockRequestedEventArgs e)
        {
            // This blocks the thread, same as Thread.Sleep
            DoSomethingAsync().Wait(); // or result
        }

        private Task DoSomethingAsync()
        {
            Thread.Sleep(5000);
            return Task.CompletedTask;
        }
    }
}
