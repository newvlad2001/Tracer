using System.Diagnostics;
using System.Threading;
using Tracer.Library.Entity;

namespace Tracer.Library.Logic.Impl
{
    public class TracerImpl : ITracer
    {
        private readonly TraceResult _traceResult = new();

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            var threadTrace = _traceResult.GetOrAddThreadTrace(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n");
            path[0] = string.Empty;

            StackFrame frame = stackTrace.GetFrames()[1];
            var className = frame.GetMethod().ReflectedType.Name;
            var methodName = frame.GetMethod().Name;

            threadTrace.AddMethod(methodName, className, string.Join(string.Empty, path));
        }

        public void StopTrace()
        {
            var threadTrace = _traceResult.GetOrAddThreadTrace(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n");
            path[0] = string.Empty;

            threadTrace.DeleteMethod(string.Join(string.Empty, path));
        }
    }
}
