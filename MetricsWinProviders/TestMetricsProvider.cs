using MetricsCommon;
using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsWinProviders
{
    public class TestMetricsProvider : IMetricsProvider
    {
        public string Name => "TestMetrincProvider";

        public List<Metric> CaptureMetrics()
        {
            return new List<Metric>();
        }
    }
}
