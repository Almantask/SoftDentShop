using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftDentShop.Infrastructure.Security.Tests
{
    public class Test
    {
        [Test]
        public void TImerTest()
        {
            var mock = new Mock<System.Timers.Timer>();
            mock.Setup(m => m.Stop()).Throws<Exception>();
        }
        

    }
}
