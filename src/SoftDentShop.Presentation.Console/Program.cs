using SoftDentShop.Domain.ApplicationCore;
using SoftDentShop.Domain.ApplicationCore.Tests;
using System;
using System.Threading.Tasks;

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

    class Test
    {

        public async Task CallAllInParallelAsync()
        {
            var t1 = FirstAsync();
            var t2 = SecondAsync();
            var t3 = ThirdAsync();

            await Task.WhenAll(t1, t2, t3);
        }

        Task FirstAsync()
        {
            return Task.CompletedTask;
        }
        Task SecondAsync()
        {
            return Task.CompletedTask;
        }
        Task ThirdAsync()
        {
            return Task.CompletedTask;
        }
    }

    class Test2
    {

        public async Task CallAllInParallelAsync()
        {
            var t1 = FirstAsync();
            var t2 = SecondAsync();
            var t3 = ThirdAsync();

            // r1, r2, r3 are running
            var r1 = await t1;
            // r2 and r3 might be done, but waiting
            var r2 = await t2;
            // r3 is waiting
            var r3 = await t3;
        }

        Task<string> FirstAsync()
        {
            return Task.FromResult("A");
        }
        Task<string> SecondAsync()
        {
            return Task.FromResult("B");
        }
        Task<string> ThirdAsync()
        {
            return Task.FromResult("C");
        }
    }

}
