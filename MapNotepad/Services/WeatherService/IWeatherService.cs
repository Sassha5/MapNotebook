using System;
using System.Threading.Tasks;
using MapNotepad.Models.Weather;

namespace MapNotepad.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetWeatherForecast(double latitude, double longitude);
    }
}
