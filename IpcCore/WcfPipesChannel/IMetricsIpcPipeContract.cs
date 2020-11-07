using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IpcCore.WcfPipesChannel
{
    [ServiceContract]
    internal interface IMetricsIpcPipeContract
    {
        [OperationContract]
        byte Register(MetricsContainer container);
    }
}
