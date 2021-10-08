namespace Tracer.Library.Logic
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        Entity.TraceResult GetTraceResult();
    }
}
