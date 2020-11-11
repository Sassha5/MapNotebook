using System;
using Newtonsoft.Json;

namespace MapNotepad.Models.Weather
{
    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
}
