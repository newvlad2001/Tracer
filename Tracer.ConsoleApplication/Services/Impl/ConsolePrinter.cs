using System;

namespace Tracer.ConsoleApplication.Services.Impl
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string data)
        {
            Console.WriteLine(data);
        }
    }
}
