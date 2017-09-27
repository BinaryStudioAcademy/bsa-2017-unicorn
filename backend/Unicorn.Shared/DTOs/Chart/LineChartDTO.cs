using Newtonsoft.Json;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Chart
{
    public class LineChartDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("series")]
        public List<ChartPointDTO> Series { get; set; }
    }
}
