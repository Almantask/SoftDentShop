using Newtonsoft.Json;
using NUnit.Framework;
using SoftDentShop.Domain.Models.StockRates;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor.Tests
{
    [TestFixture]
    public class DeserializeStockTest
    {
        [Test]
        public void Deserialize_Stock_Ok()
        {
            var json = File.ReadAllText("SampleStock.json");
            var stockRatesFull = JsonConvert.DeserializeObject<StockRateFull>(json);
            
            Assert.IsNotNull(stockRatesFull.StockRates);
        }
    }

}
