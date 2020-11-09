using MetricsCommon.Models;
namespace MetricsCommon.Collections
{
    public interface IMetricStream
    {
        void Append(MetricBase metric);
    }
}
