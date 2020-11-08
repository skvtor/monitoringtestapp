using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsHub
{
    public interface IAggregationContainer
    {
        void Aggregate(MetricBase metric);
        object GetAggregation();
    }
}
