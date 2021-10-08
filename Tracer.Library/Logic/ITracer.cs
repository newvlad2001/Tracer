using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Library.Logic
{
    interface ITracer
    {
        void StartTrace();

        void StopTrace();

        Entity.TraceResult GetTraceResult();
    }
}
