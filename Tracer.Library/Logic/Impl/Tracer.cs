using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer.Library.Entity;

namespace Tracer.Library.Logic.Impl
{
    class Tracer : ITracer
    {
        private readonly TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult(new ConcurrentDictionary<int, ThreadTrace>());
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            _traceResult[id] = new ThreadTrace(id);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = "";
            var className = stackTrace.GetFrames()[1].GetMethod().ReflectedType.Name;
            var methodName = stackTrace.GetFrames()[1].GetMethod().Name;

            _traceResult[id].AddMethod(methodName, className, string.Join("", path));
        }

        public void StopTrace()
        {
            var threadTrace = _traceResult[Thread.CurrentThread.ManagedThreadId];

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = "";

            threadTrace.DeleteMethod(string.Join("", path));
        }
    }
}
