using System;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.ApplicationCore.StockMonitor
{
    public interface IStocksMonitoringRoutine
    {
        event EventHandler<OnStockReportEndedEventArgs> ReportingEnded;
        event EventHandler<OnStockReportStartedEventArgs> ReportingStarted;

        Task StartAsync();
        void Stop();
    }
}