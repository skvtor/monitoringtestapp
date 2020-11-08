using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IpcCore.WcfPipesChannel
{
    [DataContract]
    internal class MetricsContainer
    {
        [DataMember]
        public byte[] MetricsSerialized { get; set; }
    }
}
