using MetricsCommon.Models;
using MetricsCommon.Serialization;
using System;
using System.ServiceModel;

namespace IpcCore.WcfPipesChannel
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class WcfPipeServer: IIpcServer, IMetricsIpcPipeContract
    {
        string _uri;
        IMetricsIpcPipeContract _channel;
        ServiceHost _host;
        MetricsSerializator _serializator;

        public event Action<MetricBase> OnMetricsHasArrived;

        public WcfPipeServer(string uri)
        {
            _serializator = new MetricsSerializator();
            _uri = uri;
        }

        public void Register(MetricsContainer container)
        {
            if(container != null)
            {
                var metric = _serializator.Desererialize(container.MetricsSerialized);
                OnMetricsHasArrived?.Invoke(metric);
            }
        }

        public void Start()
        {
            _host = new ServiceHost(this, new Uri(_uri));
            _host.AddServiceEndpoint(typeof(IMetricsIpcPipeContract), new NetNamedPipeBinding(), "");
            _host.Open();
        }
    }
}
