using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsRestService.Models
{
    public class ProcessesMetricsModelOut
    {
        public int MaxCpuUsagePid { get; set; }
        public double MaxCpuUsage { get; set; }
        public int MaxMemoryUsagePid { get; set; }
        public double MaxMemoryUsage { get; set; }
    }
}
