using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcCore
{
    public interface IIpcClient
    {
        void ReportMetrics(List<Metric> metrics);
    }
}
