using MetricsCommon.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MetricsCommon.Serialization
{
    public class MetricsSerializator
    {
        private BinaryFormatter _formatter;

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
