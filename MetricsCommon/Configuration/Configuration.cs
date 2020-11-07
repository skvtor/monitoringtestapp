using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Configuration
{
    public class Configuration
    {
        public string Version { get; set; }
        public Dictionary<string, string> ConfigItems { get; set; }
    }
}
