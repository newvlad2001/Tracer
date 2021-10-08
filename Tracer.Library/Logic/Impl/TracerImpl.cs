using System.Diagnostics;
using System.Threading;
using Tracer.Library.Entity;

namespace Tracer.Library.Logic.Impl
{
    public class TracerImpl : ITracer
    {
        private readonly TraceResult _traceResult;

        public TracerImpl()
        {
            _traceResult = new TraceResult();
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            var threadTrace = _traceResult.GetThreadTrace(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = "";
            var className = stackTrace.GetFrames()[1].GetMethod().ReflectedType.Name;
            var methodName = stackTrace.GetFrames()[1].GetMethod().Name;

            threadTrace.AddMethod(methodName, className, string.Join("", path));
        }

        public void StopTrace()
        {
            var threadTrace = _traceResult.GetThreadTrace(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();
            var path = stackTrace.ToString().Split("\r\n", System.StringSplitOptions.None);

            path[0] = "";

            threadTrace.DeleteMethod(string.Join("", path));
        }
    }
}
