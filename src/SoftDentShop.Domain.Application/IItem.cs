using SoftDentShop.Domain.Models.Shop;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Domain.ApplicationCore
{
    interface IItem
    {
        void Use();
    }

    class Laptop : IItem
    {
        public void Use()
        {
            //..
        }
    }

    class Stone:IItem
    {
        private ThirdPartyStone _stone;

        public Stone()
        {
        }

        public Stone(ThirdPartyStone stone)
        {
            _stone = stone;
        }

        public void Use()
        {
            _stone.Use();
        }
    }

    class Test
    {
        public static void Main()
        {
            var stone = new Stone();
            var laptop = new Laptop();
            
            var items = new IItem[]{ stone, laptop};
        }
    }
    
}