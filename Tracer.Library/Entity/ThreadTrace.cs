using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Library.Entity
{
    [XmlType("thread")]
    public class ThreadTrace
    {
        [JsonIgnore, XmlAttribute("id")]
        public int ThreadId { get; set; }

        [JsonPropertyName("time"), XmlAttribute("time")]
        public long ThreadTime { get; set; }

        [JsonPropertyName("methods"), XmlElement("methods")]
        public List<MethodInfo> MethodsInfo { get; set; } = new();

        public ThreadTrace(int threadId)
        {
            ThreadId = threadId;
        }

        public ThreadTrace() { }

        public void AddMethod(MethodInfo method)
        {
            MethodsInfo.Add(method);
        }

        public void DeleteMethod(string methodPath)
        {
            var index = MethodsInfo.FindLastIndex(item => item.MethodPath == methodPath);

            if (index != MethodsInfo.Count - 1)
            {
                var length = MethodsInfo.Count - 1 - index;
                var inners = MethodsInfo.GetRange(index + 1, length);

                MethodsInfo.RemoveRange(index + 1, length);
                MethodsInfo[index].InnerMethods = inners;
            }

            MethodsInfo[index].CalcTime();
            ThreadTime += MethodsInfo[index].Time;
        }
    }
}
