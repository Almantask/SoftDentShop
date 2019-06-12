using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models
{
    class Price:IPrice
    {
        public decimal BasePrice { get; set; }
        public ISaleStrategy OngoingSale { get; set; }

        public Price(decimal basePrice)
        {
            BasePrice = basePrice;
            OngoingSale = new NoSale();
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
