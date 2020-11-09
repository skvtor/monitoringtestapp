using System;

namespace MetricsCommon.Models
{
    [Serializable]
    public abstract class MetricBase
    {
        public abstract string TypeId { get; }
        public DateTime TimeStamp { get; set; }
    }
}
