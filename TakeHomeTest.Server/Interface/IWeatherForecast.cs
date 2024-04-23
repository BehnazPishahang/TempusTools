using Microsoft.EntityFrameworkCore;
using TakeHomeTest.Server.Domain;

namespace TakeHomeTest.Server.Interface
{
    public interface IWeatherForecastService
    {
        static string GetSummary(int temperatureC) { return ""; }
        Task<ICollection<WeatherForecast>> GetAllWeatherForecasts();
        Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast);
        Task<bool> DeleteWeatherForecast(Guid Id);
        Task<WeatherForecast> CreateWeatherForecast(WeatherForecast weatherForecast);
        Task<ICollection<WeatherForecast>> GetWeatherForecastByLocationName(string LocationName);
    }
}
