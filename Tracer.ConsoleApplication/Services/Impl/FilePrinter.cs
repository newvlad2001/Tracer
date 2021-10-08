using System.IO;

namespace Tracer.ConsoleApplication.Services.Impl
{
    public class FilePrinter : IPrinter
    {
        public string Path { get; set; }

        public FilePrinter(string path)
        {
            Path = path;
        }

        public void Print(string data)
        {
            using (var sw = new StreamWriter(Path))
            {  
                sw.WriteLine(data);
            }
        }
    }
}
