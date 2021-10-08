using System.Threading;
using Tracer.ConsoleApplication.Services;
using Tracer.ConsoleApplication.Services.Impl;
using Tracer.Library.Logic;
using Tracer.Library.Logic.Impl;
using Tracer.Library.Services.Serialization.Impl;

namespace Tracer.ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new TracerImpl();
            var thread = new Thread(Method);
            var foo = new Foo(tracer);

            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();

            var result = new JsonSerializer().Serialize(tracer.GetTraceResult());
            IPrinter printer = new FilePrinter("./json_result.json");

            printer.Print(result);
            printer = new ConsolePrinter();
            printer.Print(result);

            result = new XmlSerializer().Serialize(tracer.GetTraceResult());
            printer = new FilePrinter("./xml_result.xml");

            printer.Print(result);
            printer = new ConsolePrinter();
            printer.Print(result);

        }

        public static void Method(object obj)
        {
            var tracer = (TracerImpl)obj;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
}
