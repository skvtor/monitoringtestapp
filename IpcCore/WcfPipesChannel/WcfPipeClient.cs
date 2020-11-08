using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Threading.Tasks;
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

        public void ReportMetric(MetricBase metric)
        {
            var metricSerialized = new MetricsContainer
            {
                MetricsSerialized = _serializator.Serialize(metric)
            };

            channel.Register(metricSerialized);
        }
    }
}
