using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Unicorn.Shared.services.interfaces
{
    public interface ISerializer
    {
        T Deserialize<T>(string json, JsonSerializerSettings settings = null);
        string Serialize<T>(T value, JsonSerializerSettings settings = null);
    }
}
