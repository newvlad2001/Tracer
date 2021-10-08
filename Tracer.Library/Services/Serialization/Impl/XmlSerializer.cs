using System.IO;
using System.Linq;
using System.Xml;
using Tracer.Library.Entity;
using SystemXml = System.Xml.Serialization;

namespace Tracer.Library.Services.Serialization.Impl
{
    public class XmlSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            var data = traceResult.ThreadTraces.Values.ToArray();
            var xmlSerializer = new SystemXml.XmlSerializer(data.GetType());
            var stringWriter = new StringWriter();

            using (var writer = new XmlTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;
                xmlSerializer.Serialize(writer, data);
            }

            var result = stringWriter.ToString();
            stringWriter.Dispose();
            return result;
        }
    }
}
