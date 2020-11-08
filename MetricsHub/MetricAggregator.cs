using MetricsCommon;
using MetricsCommon.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsHub
{
    internal class MetricAggregator
    {
        Thread _thread;
        AutoResetEvent _onNewMetric;
        ConcurrentQueue<MetricBase> _metricsToAggregate = new ConcurrentQueue<MetricBase>();
        Dictionary<string, IAggregationContainer> _containers = new Dictionary<string, IAggregationContainer>();

        public MetricAggregator()
        {
            _containers.Add(Constants.Metric_ProcessDescriptor, new TestProcessMetricAggregation());
        }

        public void Start()
        {
            _onNewMetric = new AutoResetEvent(false);
            _thread = new Thread(AggregationLoop);
            _thread.Start();
        }
        public void Stop()
        {
            //todo
        }

        public void Aggregate(MetricBase metric)
        {
            _metricsToAggregate.Enqueue(metric);
            _onNewMetric.Set();
        }

        public object GetAggregation(string typeId)
        {
            return _containers[typeId].GetAggregation();
        }

        private void AggregationLoop()
        {
            MetricBase metric;
            while (true)
            {
                while(_metricsToAggregate.TryDequeue(out metric))
                {
                    var aggregationContainer = GetContainerByType(metric.TypeId);
                    if(aggregationContainer != null)
                        aggregationContainer.Aggregate(metric);
                }

                _onNewMetric.WaitOne();
            }
        }

        private IAggregationContainer GetContainerByType(string typeId)
        {
            IAggregationContainer retVal;
            if (_containers.TryGetValue(typeId, out retVal))
                return retVal;

            return null;
        }

    }
}
