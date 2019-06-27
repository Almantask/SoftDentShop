using System;
using System.Collections.Generic;

namespace SoftDentShop.Domain.Application.Tests
{
    public interface IPrinter
    {
        IEnumerable<Page> Print(int count);
    }

    public class ColoredPrinter : IPrinter
    {
        private IPrinter _printer;

        public ColoredPrinter(IPrinter printer)
        {
            _printer = printer;
        }

        public IEnumerable<Page> Print(int count)
        {
            PromptColorPick();
            return _printer.Print(count);
        }

        private void PromptColorPick()
        {
            Console.WriteLine("Picking text color..");
        }
    }

    public class PaidPrinter : IPrinter
    {
        private IPrinter _printer;
        private decimal _pagePrintPrice;

        public PaidPrinter(IPrinter printer, decimal pagePrintPrice)
        {
            _printer = printer;
            _pagePrintPrice = pagePrintPrice;
        }

        public IEnumerable<Page> Print(int count)
        {
            ChargeMoney(count);
            return _printer.Print(count);
        }

        private void ChargeMoney(int pages)
        {
            Console.WriteLine($"Please pay {_pagePrintPrice * pages}");
        }
    }

    public class WifiPrinter : IPrinter
    {
        private IPrinter _printer;

        public WifiPrinter(IPrinter printer)
        {
            _printer = printer;
        }

        public IEnumerable<Page> Print(int count)
        {
            PromptToConnect();
            return _printer.Print(count);
        }

        private void PromptToConnect()
        {
            Console.WriteLine("Connecting to wifi..");
        }
    }


}