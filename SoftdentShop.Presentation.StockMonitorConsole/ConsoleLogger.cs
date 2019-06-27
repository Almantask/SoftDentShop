using SoftDentShop.Domain.Application;

namespace SoftDentShop.Presentation.Plain
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}