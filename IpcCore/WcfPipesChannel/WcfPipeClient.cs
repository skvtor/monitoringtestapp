using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using MetricsCommon.Models;
using MetricsCommon.Serialization;

namespace IpcCore.WcfPipesChannel
{
    internal class WcfPipeClient : IIpcClient
    {
        string _uri;
        IMetricsIpcPipeContract channel;
        MetricsSerializator _serializator;
        public WcfPipeClient(string uri)
        {
            _uri = uri;
            _serializator = new MetricsSerializator();
        }

        public void Connect()
        {
            ChannelFactory<IMetricsIpcPipeContract> pipeFactory = new ChannelFactory<IMetricsIpcPipeContract>(
                new NetNamedPipeBinding(),
                new EndpointAddress(_uri));

            channel = pipeFactory.CreateChannel();
        }

        public void ReportMetrics(List<MetricBase> metrics)
        {
            foreach (var metric in metrics)
            {
                var metricSerialized = new MetricsContainer
                {
                    MetricsSerialized = _serializator.Serialize(metric)
                };

                channel.Register(metricSerialized);
            }

            //if(containers.Any())
            //    channel.Register(containers);
        }
    }
}
