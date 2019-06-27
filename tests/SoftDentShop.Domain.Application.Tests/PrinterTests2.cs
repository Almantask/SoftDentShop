using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftDentShop.Domain.Application.Tests
{
    [TestFixture]
    class PrinterTests2
    {
        [Test]
        public void TestBasicPrint()
        {
            var printer = new Printer();
            const int pagesToPrint = 5;
            var pages = printer.Print(pagesToPrint);
            Assert.AreEqual(pagesToPrint, pages.Count());
        }
    }
}
