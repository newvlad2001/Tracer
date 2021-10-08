using System;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Tracer.Library.Entity
{
    public class TraceResult
    {
        [JsonPropertyName("threads")]
        public ConcurrentDictionary<int, ThreadTrace> ThreadTraces { get; } = new();

        public ThreadTrace this[int id]
        {
            get
            {
                if (!ThreadTraces.TryGetValue(id, out var trace))
                {
                    throw new ArgumentException($"No element with id = {id}.");
                }

                return trace;
            }
            set
            {
                if (!ThreadTraces.TryAdd(id, value))
                {
                    throw new ArgumentException($"Element with id = {id} already exists.");
                }
            }
        }

        public ThreadTrace GetThreadTrace(int id)
        {
            return ThreadTraces.GetOrAdd(id, new ThreadTrace(id));
        }
    }
}
