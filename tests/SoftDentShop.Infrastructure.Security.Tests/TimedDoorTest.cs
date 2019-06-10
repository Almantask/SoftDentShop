using Moq;
using NUnit.Framework;
using SoftDentShop.Infrastructure.Security.Interfaces;
using SoftDentShop.Infrastructure.Security.Models;

namespace Tests
{
    [NonParallelizable()]
    [Category("No name")]
    public class Tests
    {
        private ITimedDoor _timedDoor;

        private Mock<ITimer> _timerMock;

        private Mock<IAlarm> _alarmMock;
        private Mock<IDoor> _doorMock;

        [SetUp]
        public void Setup()
        {
            _timerMock = new Mock<ITimer>();
            _doorMock = new Mock<IDoor>();

            _alarmMock = new Mock<IAlarm>();
            _alarmMock.SetupGet(x => x.IsEnabled).Returns(true);
            _timedDoor = new TimedDoor(_timerMock.Object, _alarmMock.Object, _doorMock.Object);
        }

        [Test]
        [Explicit]
        public void Timer_IsOn_AfterDoorOpen()
        {
            _timedDoor.Open();

            _timerMock.Verify(x => x.Run(), Times.Once());
        }

        [Test]
        [Ignore("Why not?")]
        public void Timer_IsOff_AfterDoorClosed()
        {
            _timedDoor.Open();
            _timedDoor.Close();
            
            _timerMock.Verify(x => x.Stop(), Times.Once());
        }

        [Test, Order(1)]
        [Category("WHy not")]
        public void WhyNot() { }


        [Test]
        [Repeat(10000)]
        public void Alarm_Triggered_AfterTimerPassed()
        {
            _timerMock.Setup(x => x.Run()).Raises(x => x.TimePassed += null, this, null);

            _timedDoor.Open();

            _timerMock.Verify(t => t.Run(), Times.Once());
            _alarmMock.Verify(x => x.Start(), Times.Once());
        }

        [Test, Order(2)]
        public void Alarm_DidNotFire_AfterDoorClosed()
        {
            _timedDoor.Open();
            _timedDoor.Close();

            _timerMock.Verify(x => x.Run(), Times.Once());
            _timerMock.Verify(x => x.Stop(), Times.Once());
            _alarmMock.Verify(x => x.Start(), Times.Never());
        }
    }
}