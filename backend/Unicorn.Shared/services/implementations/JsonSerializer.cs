using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unicorn.Shared.services.interfaces;

namespace Unicorn.Shared.services.implementations
{
    class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string json, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public string Serialize<T>(T value, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
