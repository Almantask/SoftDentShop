using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Interfaces
{
    public interface IAlarm
    {
        bool IsEnabled { get; set; }
        void Start();
    }
}
