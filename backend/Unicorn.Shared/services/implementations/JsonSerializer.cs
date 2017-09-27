using Newtonsoft.Json;
using Unicorn.Shared.services.interfaces;

namespace Unicorn.Shared.services.implementations
{
    class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string json, JsonSerializerSettings settings = null) => JsonConvert.DeserializeObject<T>(json, settings);

        public string Serialize<T>(T value, JsonSerializerSettings settings = null) => JsonConvert.SerializeObject(value, settings);
    }
}
