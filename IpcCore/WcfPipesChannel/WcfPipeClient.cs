using System.Collections.Generic;
using System.ServiceModel;
using MetricsCommon.Models;

namespace IpcCore.WcfPipesChannel
{
    internal class WcfPipeClient : IIpcClient
    {
        string _uri;
        IMetricsIpcPipeContract channel;
        public WcfPipeClient(string uri)
        {
            _uri = uri;
        }

        public void Connect()
        {
            ChannelFactory<IMetricsIpcPipeContract> pipeFactory = new ChannelFactory<IMetricsIpcPipeContract>(
                new NetNamedPipeBinding(),
                new EndpointAddress(_uri));

            channel = pipeFactory.CreateChannel();
        }

        public void ReportMetrics(List<Metric> metrics)
        {
            var code = channel.Register(new MetricsContainer());
        }
    }
}
