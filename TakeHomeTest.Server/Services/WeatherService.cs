using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TakeHomeTest.Server.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly DatabaseContext _context;
        public WeatherForecastService(
             DatabaseContext context
        )
        {
            _context = context;
        }

        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static string GetSummary(int temperatureC) {
            if (temperatureC <= 0) return Summaries[0];
            if (temperatureC >= 45) return Summaries[9];
            return Summaries[temperatureC / 5];

        }

        public async Task<ICollection<WeatherForecast>> GetAllWeatherForecasts()
        {
            return await _context.WeatherForecast.ToListAsync();
        }
        public async Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            //TODO Implement updating a weatherForcast in the database
            return null;
        }
        public async Task<bool> DeleteWeatherForecast(Guid Id)
        {
            //TODO implement deleting a weatherForcast from the database
            return false;
        }
        public async Task<WeatherForecast> CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            //TODO implement inserting weatherForcast into database
            return null;
        }

    }
}
