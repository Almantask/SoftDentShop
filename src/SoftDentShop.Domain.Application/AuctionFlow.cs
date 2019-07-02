using SoftDentShop.Domain.Models.Shop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.ApplicationCore
{
    public class AuctionFlow
    {
        private Item[] _items;
        private Person[] _people;
        private ILogger _logger;

        public AuctionFlow(int itemsCount, int peopleCount, ILogger logger)
        {
            _items = new Item[itemsCount];
            _people = new Person[peopleCount];
            _logger = logger;

            RandomizeNames();
        }

        private void RandomizeNames()
        {
            for (int i = 0; i < _people.Length; i++)
            {
                _people[i] = new Person("Person" + i.ToString());
            }
        }

        public void PrintAll()
        {
            var stopwatch = new Stopwatch();
            _logger.Log($"Started: {DateTime.Now}");
            stopwatch.Start();
            var result = Parallel.ForEach(_people, (person) =>
            {
                Thread.Sleep(100);
                Print(person);
            });

            _logger.Log($"Ended: {DateTime.Now}");
            stopwatch.Stop();
            _logger.Log($"Total time elapsed: {(float)stopwatch.ElapsedMilliseconds / 1000}");
        }

        private void Print(Person person)
        {
            _logger.Log($"{person.Name}");
        }



    }
}
