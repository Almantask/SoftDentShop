using SoftDentShop.Infrastructure.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Models
{
    public class Alarm : IAlarm
    {
        public bool IsEnabled { get; set; }

        public void Start()
        {
        }
    }
}
