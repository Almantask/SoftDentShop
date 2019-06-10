using Moq;
using NUnit.Framework;

namespace SoftDentShop.Infrastructure.API.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var stub = new Mock<IFoo>();
            stub.Setup(g => g.GetRandom()).Returns(true);
            stub.Setup(g => g.GetSomething(It.IsAny<object>()))
                .Returns(true);

            var foo = stub.Object;
            var result = foo.GetRandom();
            Assert.IsTrue(result);
        }
    }

    public interface IFoo
    {
        void Bar();
        bool GetSomething(object id);
        bool GetRandom();
    }

    class Example
    {
        private IFoo _foo;

        public Example(IFoo foo)
        {
            _foo = foo;
        }

        public bool Random()
        {
            return _foo.GetRandom();
        }
    }
}
