using IpcCore;
using MetricsCommon;
using MetricsCommon.Collections;
using MetricsCommon.Configuration;
using MetricsCommon.Models;
using MetricsWinProviders;
using System;
using System.Collections.Concurrent;
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
        Timer _timer = null;
        IIpcClient _client;
        Thread _thread;
        ConcurrentQueue<MetricBase> _queue = new ConcurrentQueue<MetricBase>();
        AutoResetEvent _onNewMessage = new AutoResetEvent(false);
        SemaphoreSlim _onTickEvent = new SemaphoreSlim(0);
        

        public SimpleMetricMonitor(Configuration config)
        {
            _config = config;
            _metricsProviders = new List<IMetricsProvider>();
            _metricsProviders.Add(new TestMetricsProvider());
        }

        public async Task Run()
        {
            _thread = new Thread(MetricsSendingLoop);
            _thread.Start();

            var periodMs = int.Parse(_config.ConfigItems[Constants.Config_MetricMeasurmantPeriod]);

            _client = IpcConnector.Connect(_config);
            _timer = new Timer(OnTimerTick, null, periodMs, periodMs);

            await MetricsMeasurmentLoop();
        }
        public void Stop()
        {
            //todo
        }

        private async Task MetricsMeasurmentLoop()
        {
            var metricStream = new MetricStreamProcessor(OnMetricHasArrived);
            //todo
            while (true)
            {
                foreach (var metricsProvider in _metricsProviders)
                    metricsProvider.CaptureMetrics(metricStream);

                await _onTickEvent.WaitAsync();
            }
        }
        private void MetricsSendingLoop()
        {
            while (true)
            {
                MetricBase metric;
                if (_queue.TryDequeue(out metric))
                    _client.ReportMetric(metric);

                _onNewMessage.WaitOne();
            }
        }

        private void OnTimerTick(object state)
        {
            _onTickEvent.Release();
        }
        private void OnMetricHasArrived(MetricBase metric)
        {
            _queue.Enqueue(metric);
            _onNewMessage.Set();
        }
    }
}
