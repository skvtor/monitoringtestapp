using System;

namespace MetricsCommon.Models
{
    [Serializable]
    public class ProcessDescriptorMetric : MetricBase
    {
        public override string TypeId => Constants.Metric_ProcessDescriptor;
        public string Name { get; set; }
        public int Pid { get; set; }
        public int SessionId { get; set; }
        public double CpuPerc { get; set; }
        public double MemoryUsage { get; set; }
        public double DiscUsage { get; set; }
        public double NetworkUsage { get; set; }
    }
}
