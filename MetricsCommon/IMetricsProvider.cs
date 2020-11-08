using MetricsCommon.Collections;
using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon
{
    public interface IMetricsProvider
    {
        string Name { get; }
        void CaptureMetrics(IMetricStream stream);
    }
}
