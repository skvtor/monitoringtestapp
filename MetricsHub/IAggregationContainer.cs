using MetricsCommon.Models;

namespace MetricsHub
{
    public interface IAggregationContainer
    {
        void Aggregate(MetricBase metric);
        object GetAggregation();
    }
}
