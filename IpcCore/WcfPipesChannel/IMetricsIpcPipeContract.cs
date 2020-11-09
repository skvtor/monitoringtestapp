using System.ServiceModel;

namespace IpcCore.WcfPipesChannel
{
    [ServiceContract]
    internal interface IMetricsIpcPipeContract
    {
        [OperationContract]
        void Register(MetricsContainer metric);
    }
}
