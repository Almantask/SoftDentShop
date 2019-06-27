using Moq;
using NUnit.Framework;
using System.Linq;

namespace SoftDentShop.Domain.Application.Tests
{
    [TestFixture]
    public class PrinterTests
    {
        [Test]
        public void Basic_Print()
        {
            var printer = new Printer();
            const int pagesToPrint = 5;
            var pages = printer.Print(pagesToPrint);
            Assert.AreEqual(pagesToPrint, pages.Count());
        }

        [Test]
        public void Colorful_Print()
        {
            const int pagesToPrint = 5;

            var printerMock = new Mock<IPrinter>();
            var colorfulPrinter = new ColoredPrinter(printerMock.Object);
            var pages = colorfulPrinter.Print(pagesToPrint);
            printerMock.Verify(p => p.Print(pagesToPrint), Times.Once);
        }

    }
}
