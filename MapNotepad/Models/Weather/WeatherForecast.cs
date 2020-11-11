using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MapNotepad.Models.Weather
{
    public class WeatherForecast
    {
        [JsonProperty("cod")]
        public long Cod { get; set; }

        [JsonProperty("message")]
        public long Message { get; set; }

        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("list")]
        public List<WeatherData> List { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }
}
