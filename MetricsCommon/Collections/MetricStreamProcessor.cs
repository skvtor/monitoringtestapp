using System;
using MetricsCommon.Models;

namespace MetricsCommon.Collections
{
    public class MetricStreamProcessor : IMetricStream
    {
        private Action<MetricBase> _processor;
        
        public MetricStreamProcessor(Action<MetricBase> processor)
        {
            _processor = processor;
        }

        public void Append(MetricBase metric)
        {
            _processor(metric);
        }
    }
}
