using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MapNotepad.Models
{
    public class WeatherForecast
    {
        [JsonProperty("list")]
        public List<WeatherData> List { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("dt_txt")]
        public string Date { get; set; }

        [JsonIgnore]
        public string DisplayDate => DateTime.Parse(Date).ToLocalTime().ToString("g");
        [JsonIgnore]
        public string DisplayTemp => $"Temp: {Main?.Temperature ?? 0}° {Weather?[0]?.Visibility ?? string.Empty}";
        [JsonIgnore]
        public string DisplayIcon => $"http://openweathermap.org/img/w/{Weather?[0]?.Icon}.png";
    }

    public class Weather
    {
        [JsonProperty("main")]
        public string Visibility { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }
}
