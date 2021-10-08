using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Library.Entity
{
    class TraceResult
    {
        public ConcurrentDictionary<int, ThreadTrace> ThreadTraces { get; }

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

        public TraceResult(ConcurrentDictionary<int, ThreadTrace> traces)
        {
            ThreadTraces = traces;
        }
    }
}
