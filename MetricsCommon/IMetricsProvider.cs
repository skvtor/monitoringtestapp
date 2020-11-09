using MetricsCommon.Collections;

namespace MetricsCommon
{
    public interface IMetricsProvider
    {
        string Name { get; }
        void CaptureMetrics(IMetricStream stream);
    }
}
