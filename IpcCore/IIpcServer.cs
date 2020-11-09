using MetricsCommon.Models;
using System;

namespace IpcCore
{
    public interface IIpcServer
    {
        void Start();
        event Action<MetricBase> OnMetricsHasArrived;
    }
}
