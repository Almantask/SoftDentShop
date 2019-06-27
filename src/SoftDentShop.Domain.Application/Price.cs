using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.Shop
{
    class Price:IPrice
    {
        public decimal BasePrice { get; set; }
        public ISaleStrategy OngoingSale { get; set; }

        public Price(decimal basePrice)
        {
            BasePrice = basePrice;
#pragma warning disable CS0436 // Type conflicts with imported type
            OngoingSale = new NoSale();
#pragma warning restore CS0436 // Type conflicts with imported type
        }

        public Price(decimal basePrice, ISaleStrategy salesStrategy)
        {
            BasePrice = basePrice;
            OngoingSale = salesStrategy;
        }

        public decimal GetActualPrice(CustomerAccountType accoutType)
        {
            return OngoingSale.Calculate(BasePrice, accoutType);
        }
    }
}
