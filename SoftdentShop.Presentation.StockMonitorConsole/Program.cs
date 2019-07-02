using SoftDentShop.Domain.Application;
using SoftDentShop.Domain.Application.StockMonitor;
using SoftDentShop.Presentation.Plain;
using System;

namespace SoftdentShop.Presentation.StockMonitorConsole
{
    class Program
    {
        private static IStocksMonitoringRoutine _stockMonitoringRoutine;
        private static ILogger _logger;

        static Program()
        {
            _logger = new ConsoleLogger();
        }

        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            try
            {
                _stockMonitoringRoutine = BuildStockMonitoringRoutine();
                _stockMonitoringRoutine.Start();
                Console.WriteLine("Press any key to terminate");
                Console.ReadKey();
                _stockMonitoringRoutine.Stop();
                Console.WriteLine("Report terminated. Press any key to close app.");
                Console.ReadKey();
            }
            catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                _stockMonitoringRoutine.Stop();
            }

            //CheckForStop();
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        private static void CheckForStop()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.WriteLine("z");
            }
            _stockMonitoringRoutine.Stop();
        }

        private static IStocksMonitoringRoutine BuildStockMonitoringRoutine()
        {
            var monitor = new StocksMonitor();
            monitor.StockRequested += Monitor_StockRequested;
            monitor.StockRetrieved += Monitor_StockRetrieved;

            var reporter = new StockRatesReporter(StockTimeScale.Monthly, monitor);

            var stockMonitoringRoutine = new StocksMonitoringRoutine(reporter, monitor, StockTimeScale.Minutely, "MSTF");
            stockMonitoringRoutine.ReportingStarted += StockMonitoringRoutine_ReportingStarted;
            stockMonitoringRoutine.ReportingEnded += StockMonitoringRoutine_ReportingEnded;

            return stockMonitoringRoutine;
        }

        private static void StockMonitoringRoutine_ReportingEnded(object sender, OnStockReportEndedEventArgs e)
        {
            _logger.Log($"Reporting ended: {DateTime.Now}");
        }

        private static void StockMonitoringRoutine_ReportingStarted(object sender, OnStockReportStartedEventArgs e)
        {
            _logger.Log($"Reporting started: {DateTime.Now}");
        }

        private static void Monitor_StockRetrieved(object sender, OnStockRetrievedEventArgs e)
        {
            _logger.Log($"Stock retrieved: {e.StockRateFull.MetaData.StockCode}");
        }

        private static void Monitor_StockRequested(object sender, OnStockRequestedEventArgs e)
        {
            _logger.Log($"Stock requested: {e.StockCode}");
        }
    }
}
