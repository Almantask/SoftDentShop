using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.Shop
{
    public class PriceList
    {
        // indexer...
        private readonly IDictionary<Item, IPrice> _prices;

        public PriceList(IDictionary<Item, IPrice> prices)
        {
            _prices = prices;
        }
    }
}
