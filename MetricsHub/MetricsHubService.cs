using IpcCore;
using MetricsCommon;
using MetricsCommon.Configuration;
using MetricsCommon.Models;

namespace MetricsHub
{
    public class MetricsHubService
    {
        private ConfigurationManager _configManager;
        private Configuration _config;
        private IIpcServer _ipcServer;
        private MetricAggregator _aggregator;

        public void Start(Configuration config)
        {
            _config = config;
            _configManager = new ConfigurationManager();
            _configManager.Publish(config);

            _aggregator = new MetricAggregator();
            _aggregator.Start();

            _ipcServer = IpcConnector.GetServer(config);
            _ipcServer.OnMetricsHasArrived += OnMetricsHasArrived;
            _ipcServer.Start();
        }

        public TestProcessMetrics GetTestMetrics()
        {
            return (TestProcessMetrics)_aggregator.GetAggregation(Constants.Metric_ProcessDescriptor);
        }

        private void OnMetricsHasArrived(MetricBase metric)
        {
            _aggregator.Aggregate(metric);
        }
    }
}
