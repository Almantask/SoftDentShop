using SoftDentShop.Domain.ApplicationCore;

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