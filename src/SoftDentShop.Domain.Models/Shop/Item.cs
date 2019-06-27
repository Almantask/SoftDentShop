using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models.Shop
{
    public class Item
    {
        public string Id { get; }
        public string Name { set; get; }
        public string Description { set; get; }
    }
}
