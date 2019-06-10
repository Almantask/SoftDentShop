using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Interfaces
{
    public interface ITimer : ITimerAdjuster, ITimerRunner
    {
        event EventHandler TimePassed;

        void Stop();
    }
}
