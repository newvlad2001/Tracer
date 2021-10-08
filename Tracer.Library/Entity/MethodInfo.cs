using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Library.Entity
{
    public class MethodInfo
    {
        private readonly Stopwatch _stopWatch = new();

        [JsonIgnore, XmlIgnore]
        public string MethodPath { get; }

        [JsonPropertyName("class"), XmlAttribute("class")]
        public string ClassName { get; set; }

        [JsonPropertyName("name"), XmlAttribute("name")]
        public string MethodName { get; set; }

        [JsonPropertyName("time"), XmlAttribute("time")]
        public long Time { get; set; }        

        [JsonPropertyName("methods"), XmlElement("methods")] 
        public List<MethodInfo> InnerMethods { get; set; }

        public MethodInfo() { }

        public MethodInfo(string methodName, string className, string methodPath)
        {
            ClassName = className;
            MethodName = methodName;
            MethodPath = methodPath;
            _stopWatch.Start();
        }

        public void CalcTime()
        {
            _stopWatch.Stop();
            Time = _stopWatch.ElapsedMilliseconds;
        }
    }
}
