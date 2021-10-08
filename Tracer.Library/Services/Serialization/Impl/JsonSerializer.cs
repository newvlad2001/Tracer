using Tracer.Library.Entity;
using SystemJson = System.Text.Json;

namespace Tracer.Library.Services.Serialization.Impl
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            var options = new SystemJson.JsonSerializerOptions() { WriteIndented = true };
            return SystemJson.JsonSerializer.Serialize(result, options);
        }
    }
}
