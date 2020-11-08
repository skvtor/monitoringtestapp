using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Models
{
    [Serializable]
    public abstract class MetricBase
    {
        public abstract string TypeId { get; }
        public DateTime TimeStamp { get; set; }
    }
}
