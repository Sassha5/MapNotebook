using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MapNotepad.Models;
using Newtonsoft.Json;

namespace MapNotepad.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;

        public WeatherService()
        {
            _client = new HttpClient();
        }

        #region -- IWeatherService Implementation --

        public async Task<WeatherForecast> GetWeatherForecast(double latitude, double longitude)
        {
            string requestUri = "https://api.openweathermap.org/data/2.5/forecast";
            requestUri += $"?lat={latitude}&lon={longitude}";
            requestUri += "&units=metric";
            requestUri += "&APPID=" + Constants.WeatherAPIkey;

            WeatherForecast weatherForecast = null;
            try
            {
                var response = await _client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherForecast = JsonConvert.DeserializeObject<WeatherForecast>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherForecast;
        }

        #endregion
    }
}
