using MetricsCommon.Models;
namespace IpcCore
{
    public interface IIpcClient
    {
        void ReportMetric(MetricBase metrics);
    }
}
