using MetricsCommon.Configuration;
using System.Threading.Tasks;

namespace MetricsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.GetConfig();
            var monitor = new SimpleMetricMonitor(config);
            Task.Run(async () => await monitor.Run()).Wait();
        }
    }
}
