using SoftDentShop.Domain.Application;
using SoftDentShop.Domain.Application.Tests;
using System;

namespace SoftDentShop.Presentation.Plain
{
    class Program
    {
        static void Main(string[] args)
        {
            var flow = new AuctionFlow(1000, 1000, new ConsoleLogger());
            flow.PrintAll();
            Console.ReadLine();
        }

        
    }
}
