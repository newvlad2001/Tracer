using System;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Tracer.Library.Entity
{
    public class TraceResult
    {
        [JsonPropertyName("threads")]
        public ConcurrentDictionary<int, ThreadTrace> ThreadTraces { get; } = new();

        public ThreadTrace GetOrAddThreadTrace(int id)
        {
            return ThreadTraces.GetOrAdd(id, new ThreadTrace(id));
        }
    }
}
