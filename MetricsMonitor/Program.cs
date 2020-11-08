using IpcCore;
using MetricsCommon.Configuration;
using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.GetConfig();
            var monitor = new SimpleMetricMonitor(config);
            monitor.Run();
        }
    }
}
