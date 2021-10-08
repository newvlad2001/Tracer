using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Library.Services.Serialization
{
    interface ISerializer
    {
        string Serialize(Entity.TraceResult result);
    }
}
