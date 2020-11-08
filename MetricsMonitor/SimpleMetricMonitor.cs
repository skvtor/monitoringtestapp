using IpcCore;
using MetricsCommon;
using MetricsCommon.Configuration;
using MetricsCommon.Models;
using MetricsWinProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsMonitor
{
    public class SimpleMetricMonitor
    {
        Configuration _config;
        List<IMetricsProvider> _metricsProviders;
            
        public SimpleMetricMonitor(Configuration config)
        {
            _config = config;
            _metricsProviders = new List<IMetricsProvider>();
            _metricsProviders.Add(new TestMetricsProvider());
        }

        public void Run()
        {
            var ipcClient = IpcConnector.Connect(_config);

            while (true)
            {
                var metrics = new List<MetricBase>();
                foreach(var metricsProvider in _metricsProviders)
                    metrics.AddRange(metricsProvider.CaptureMetrics());

                ipcClient.ReportMetrics(metrics);

                Thread.Sleep(3000);
            }
        }

        public void Stop()
        {
            //todo
        }
    }
}
