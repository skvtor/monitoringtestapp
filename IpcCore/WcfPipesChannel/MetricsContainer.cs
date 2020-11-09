using System.Runtime.Serialization;

namespace IpcCore.WcfPipesChannel
{
    [DataContract]
    internal class MetricsContainer
    {
        [DataMember]
        public byte[] MetricsSerialized { get; set; }
    }
}
