using Newtonsoft.Json;
using System;

namespace SoftDentShop.Domain.Models.StockRates
{
    public class StockRatesMetadata
    {
        [JsonProperty("1. Information")]
        public string Description { get; set; }
        [JsonProperty("2. Symbol")]
        public string StockCode { get; set; }
        [JsonProperty("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }
        [JsonProperty("6. Time Zone")]
        public string TimeZone { get; set; }
    }
}