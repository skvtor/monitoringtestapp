using Newtonsoft.Json;

namespace MetricsCommon.Serialization
{
    public class StringSerializator
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
