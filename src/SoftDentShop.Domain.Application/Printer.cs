using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftDentShop.Domain.Application.Tests
{
    public class Printer:IPrinter
    {
        public Printer()
        {
        }

        public IEnumerable<Page> Print(int pagesToPrint)
        {
            Console.WriteLine("Printing..");
            IList<Page> pages =  new List<Page>();
            for(var i = 0; i < pagesToPrint; i++)
            {
                var page = new Page();
                pages.Add(page);
            }
            Console.WriteLine("Printing done.");
            return pages;
        }
    }
}