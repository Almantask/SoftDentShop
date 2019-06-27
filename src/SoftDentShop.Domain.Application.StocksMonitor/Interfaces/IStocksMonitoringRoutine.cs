using System;

namespace SoftDentShop.Domain.Application.StockMonitor
{
    public interface IStocksMonitoringRoutine
    {
        event EventHandler<OnStockReportEndedEventArgs> ReportingEnded;
        event EventHandler<OnStockReportStartedEventArgs> ReportingStarted;

        void Start();
        void Stop();
    }
}