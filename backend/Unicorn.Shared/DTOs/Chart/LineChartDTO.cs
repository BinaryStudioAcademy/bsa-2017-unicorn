using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
