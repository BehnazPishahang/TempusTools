using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TakeHomeTest.Server.Services
{
    public class WeatherForecastService : IWeatherForcastService
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

        public async Task<ICollection<WeatherForecast>> GetAllWeatherForcasts()
        {
            return await _context.WeatherForecast.ToListAsync();
        }
        public async Task<WeatherForecast> UpdateWeatherForcast(WeatherForecast weatherForcast)
        {
            //TODO Implement updating a weatherForcast in the database
            return null;
        }
        public async Task<bool> DeleteWeatherForcast(Guid Id)
        {
            //TODO implement deleting a weatherForcast from database
            return false;
        }
        public async Task<WeatherForecast> CreateWeatherForcast(WeatherForecast weatherForcast)
        {
            //TODO implement inserting weatherForcast into database
            return null;
        }

    }
}
