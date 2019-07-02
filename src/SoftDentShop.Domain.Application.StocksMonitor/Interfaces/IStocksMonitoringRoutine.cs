using System;
using System.Threading.Tasks;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public interface IStocksMonitoringRoutine
    {
        event EventHandler<OnStockReportEndedEventArgs> ReportingEnded;
        event EventHandler<OnStockReportStartedEventArgs> ReportingStarted;

        Task Start();
        void Stop();
    }
}