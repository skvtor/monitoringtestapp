using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Collections
{
    public interface IMetricStream
    {
        void Append(MetricBase metric);
    }
}
