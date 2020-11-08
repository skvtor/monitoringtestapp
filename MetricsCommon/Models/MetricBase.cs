using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Models
{
    [Serializable]
    //public abstract class MetricBase
    public class MetricBase
    {
        //        public abstract  string TypeId { get; }
        //public override string TypeId => Constants.MetricProcessDescriptor;
        public string TypeId { get; set; } = Constants.MetricProcessDescriptor;
        public string Name { get; set; }
        public int Pid { get; set; }
        public int SessionId { get; set; }
        public double CpuPerc { get; set; }
        public double MemoryUsage { get; set; }
        public double DiscUsage { get; set; }
        public double NetworkUsage { get; set; }
    }
}
