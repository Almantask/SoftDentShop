using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.Shop
{
    public interface IPrice
    {
        decimal BasePrice { get; set; }
        ISaleStrategy OngoingSale { get; set; }
        decimal GetActualPrice(CustomerAccountType accoutType);
    }
}
