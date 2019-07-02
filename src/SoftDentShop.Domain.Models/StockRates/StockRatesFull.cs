using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SoftDentShop.Domain.Models.StockRates
{
    public class StockRateFull
    {
        [JsonProperty("Meta Data")]
        public StockRatesMetadata MetaData { get; set; }

        public Dictionary<DateTime, StockRate> StockRates
        {
            get => StockRatesHourly ?? StockRatesDaily ?? StockRatesWeekly ?? StockRatesMonthly;
        }
        [JsonProperty("Time Series (5min)")]
        private Dictionary<DateTime, StockRate> StockRatesHourly { get; set; }
        [JsonProperty("Time Series (Daily)")]
        private Dictionary<DateTime, StockRate> StockRatesDaily { get; set; }
        [JsonProperty("Weekly Time Series")]
        private Dictionary<DateTime, StockRate> StockRatesWeekly { get; set; }
        [JsonProperty("Monthly Time Series")]
        private Dictionary<DateTime, StockRate> StockRatesMonthly { get; set; }
    }
}