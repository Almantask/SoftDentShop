﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.Shop
{
    public class NoSale : ISaleStrategy
    {
        public decimal Calculate(decimal basePrice, CustomerAccountType accoutType)
        {
            return basePrice;
        }
    }
}
