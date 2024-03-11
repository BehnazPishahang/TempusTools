using Microsoft.EntityFrameworkCore;

namespace TakeHomeTest.Server
{
    public interface IWeatherForecastService
    {
        static string GetSummary(int temperatureC) { return ""; }
        Task<ICollection<WeatherForecast>> GetAllWeatherForecasts();
        Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast);
        Task<bool> DeleteWeatherForecast(Guid Id);
        Task<WeatherForecast> CreateWeatherForecast(WeatherForecast weatherForecast);
    }
}
