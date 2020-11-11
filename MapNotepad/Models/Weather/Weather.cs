using System;
using Newtonsoft.Json;

namespace MapNotepad.Models.Weather
{
    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Visibility { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
