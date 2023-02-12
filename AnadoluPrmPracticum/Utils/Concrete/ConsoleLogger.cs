using AnadoluPrmPracticum.Utils.Abstract;

namespace AnadoluPrmPracticum.Utils.Concrete
{
    public class ConsoleLogger : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
