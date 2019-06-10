using SoftDentShop.Infrastructure.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Models
{
    public class TimedDoor : ITimedDoor
    {
        private ITimer _timer { get; set; }

        private IAlarm _alarm { get; set; }

        private IDoor _door { get; set; }


        public TimedDoor(ITimer timer, IAlarm alarm, IDoor door)
        {
            _timer = timer;
            _alarm = alarm;
            _door = door;

            const int defaultTime = 30;
            _timer.SetTime(defaultTime);

            _timer.TimePassed += ((object e, EventArgs args) =>
            {
                _alarm.Start();
            });
        }

        public void Close()
        {
            _door.Close();
            _timer.Stop();
        }

        public void Open()
        {
            _door.Open();
            if (_alarm.IsEnabled)
            {
                _timer.Run();
            }
        }

        public void SetTime(int time)
        {
            _timer.SetTime(time);
        }

        public void ToggleAlarm()
        {
            _alarm.IsEnabled = !_alarm.IsEnabled;
        }
    }
}
