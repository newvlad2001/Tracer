using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Library.Entity
{
    [XmlType("thread")]
    public class ThreadTrace
    {
        [JsonPropertyName("id"), XmlAttribute("id")] 
        public int ThreadId { get; set; }

        [JsonPropertyName("time"), XmlAttribute("time")] 
        public long ThreadTime { get; set; }

        [JsonPropertyName("methods"), XmlElement("methods")] 
        public List<MethodInfo> MethodsInfo { get; set; }

        public ThreadTrace(int threadId)
        {
            MethodsInfo = new List<MethodInfo>();
            ThreadId = threadId;
        }

        public ThreadTrace() { }

        public void AddMethod(string methodName, string className, string methodPath)
        {
            MethodsInfo.Add(new MethodInfo(methodName, className, methodPath));
        }

        public void DeleteMethod(string methodPath)
        {
            var index = MethodsInfo.FindLastIndex(item => item.MethodPath == methodPath);

            if (index != MethodsInfo.Count - 1)
            {
                var length = MethodsInfo.Count - 1 - index;
                var childs = MethodsInfo.GetRange(index + 1, length);

                for (var i = 0; i < length; i++)
                {
                    MethodsInfo.RemoveAt(MethodsInfo.Count - 1);
                }

                MethodsInfo[index].InnerMethods = childs;
            }

            MethodsInfo[index].CalcTime();
            ThreadTime += MethodsInfo[index].Time;
        }
    }
}
