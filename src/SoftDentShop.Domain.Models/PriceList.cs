using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models
{
    public class PriceList
    {
        // indexer...
        private IDictionary<Item, IPrice> _prices;
    }
}
