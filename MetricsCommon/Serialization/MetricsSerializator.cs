using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Serialization
{
    public class MetricsSerializator
    {
        BinaryFormatter _formatter;
        public MetricsSerializator()
        {
            _formatter = new BinaryFormatter();
        }

        public byte[] Serialize(MetricBase metric)
        {
            var memStream = new MemoryStream();
            _formatter.Serialize(memStream, metric);
            return memStream.GetBuffer();
        }

        public MetricBase Desererialize(byte[] data)
        {
            var memStream = new MemoryStream(data);
            return (MetricBase)_formatter.Deserialize(memStream);
        }
    }
}
