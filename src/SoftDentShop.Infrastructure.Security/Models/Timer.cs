using SoftDentShop.Infrastructure.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Models
{
    public class Timer : ITimer
    {
        private int _timePassed = 0;

        private int _timeToPass = 30;

        public event EventHandler TimePassed;

        public void Run()
        {
            if (_timePassed >= _timeToPass)
            {
                TimePassed?.Invoke(this, new EventArgs());
                return;
            }

            _timePassed++;

            Run();
        }

        public void SetTime(int time)
        {
            _timeToPass = time;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
