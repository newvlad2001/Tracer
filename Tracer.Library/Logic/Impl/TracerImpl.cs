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
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = string.Empty;
            var className = stackTrace.GetFrames()[1].GetMethod().ReflectedType.Name;
            var methodName = stackTrace.GetFrames()[1].GetMethod().Name;

            threadTrace.AddMethod(new MethodInfo(methodName, className, string.Join(string.Empty, path)));
        }

        public void StopTrace()
        {
            var threadTrace = _traceResult.GetOrAddThreadTrace(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = string.Empty;

            threadTrace.DeleteMethod(string.Join(string.Empty, path));
        }
    }
}
