using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetricsCommon.Configuration;
using MetricsHub;

namespace MetricsRestService
{
    class Program
    {
        static MetricsHubService _hub;

        static void Main(string[] args)
        {
            var config = new Configuration()
            {
                ConfigItems = new Dictionary<string, string>
                {
                    { "hubPipeUri","net.pipe://localhost/Pipe" }
                }
            };

            _hub = new MetricsHubService();
            var t = _hub.Start(config);
            t.Wait();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
