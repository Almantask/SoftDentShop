﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Interfaces
{
    public interface ITimedDoor : IDoor, ITimerAdjuster
    {
        void ToggleAlarm();
    }
}
