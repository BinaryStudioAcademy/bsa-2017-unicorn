using Newtonsoft.Json;

namespace Unicorn.Shared.DTOs.Chart
{
    public class ChartPointDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
