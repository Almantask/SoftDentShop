using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.Models
{
    class Stock
    {
        private IDictionary<Item, int> _quantities;

        public int this[Item item]
        {
            get
            {
                if (_quantities.ContainsKey(item))
                {
                    return _quantities[item];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (_quantities.ContainsKey(item))
                {
                    _quantities[item] = value;
                }
                else
                {
                    _quantities.Add(item, value);
                }
            }
        }
    }
}
