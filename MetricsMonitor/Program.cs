using MetricsCommon.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => MonitorBody());
            t.Wait();
        }

        static async Task MonitorBody()
        {
            var config = await ConfigurationManager.GetConfig();
            int i = 2345;
        }
    }
}
