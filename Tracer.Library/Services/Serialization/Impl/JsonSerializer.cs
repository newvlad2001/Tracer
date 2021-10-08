using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Library.Entity;
using SystemJson = System.Text.Json;

namespace Tracer.Library.Services.Serialization.Impl
{
    class JsonSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            var options = new SystemJson.JsonSerializerOptions() { WriteIndented = true };
            return SystemJson.JsonSerializer.Serialize(result, options);
        }
    }
}
