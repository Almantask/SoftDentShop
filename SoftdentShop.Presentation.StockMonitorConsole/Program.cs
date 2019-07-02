using SoftDentShop.Domain.ApplicationCore;
using SoftDentShop.Domain.ApplicationCore.StockMonitor;
using SoftDentShop.Presentation.Plain;
using System;
using System.Threading.Tasks;

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
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //ThreadException += UnhandledExceptionTrapper;
            try
            {
                Console.WriteLine("Press any key to terminate");
                _stockMonitoringRoutine = BuildStockMonitoringRoutine();
                _stockMonitoringRoutine.Start();
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
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
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
