using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IpcCore.WcfPipesChannel
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class WcfPipeServer: IIpcServer, IMetricsIpcPipeContract
    {
        string _uri;
        IMetricsIpcPipeContract channel;
        ServiceHost _host;

        public WcfPipeServer(string uri)
        {
            _uri = uri;
        }

        public byte Register(MetricsContainer container)
        {
            return 0;
        }

        public void Start()
        {
            _host = new ServiceHost(this, new Uri(_uri));
            _host.AddServiceEndpoint(typeof(IMetricsIpcPipeContract), new NetNamedPipeBinding(), "");
            _host.Open();
        }
    }
}
