using Microsoft.EntityFrameworkCore;

namespace TakeHomeTest.Server
{
    public interface IWeatherForcastService
    {
        static string GetSummary(int temperatureC) { return ""; }
        Task<ICollection<WeatherForecast>> GetAllWeatherForcasts();
        Task<WeatherForecast> UpdateWeatherForcast(WeatherForecast weatherForcast);
        Task<bool> DeleteWeatherForcast(Guid Id);
        Task<WeatherForecast> CreateWeatherForcast(WeatherForecast weatherForcast);
    }
}
