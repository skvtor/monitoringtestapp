using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCommon.Serialization
{
    public class Serializator
    {
        public static string SerializeToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string data)
            where T: class
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
