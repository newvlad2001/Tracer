using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer.Library.Entity
{
    class MethodInfo
    {
        private readonly Stopwatch _stopWatch = new();

        [JsonIgnore, XmlIgnore]
        public string MethodPath { get; }

        [JsonPropertyName("time"), XmlAttribute("time")]
        public long Time { get; private set; }

        [JsonPropertyName("name"), XmlAttribute("name")]
        public string MethodName { get; private set; }

        [JsonPropertyName("class"), XmlAttribute("class")]
        public string ClassName { get; private set; }

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

        public void CalculateTime()
        {
            _stopWatch.Stop();
            Time = _stopWatch.ElapsedMilliseconds;
        }
    }
}
