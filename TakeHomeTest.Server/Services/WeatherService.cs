using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeHomeTest.Server.Domain;
using TakeHomeTest.Server.Interface;

namespace TakeHomeTest.Server.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly DatabaseContext _context;
        public WeatherForecastService(DatabaseContext context)
        {
            _context = context;
        }

        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static string GetSummary(int temperatureC)
        {
            if (temperatureC <= 0) return Summaries[0];
            if (temperatureC >= 45) return Summaries[9];
            return Summaries[temperatureC / 5];

        }

        public async Task<ICollection<WeatherForecast>> GetAllWeatherForecasts()
        {
            return await _context.WeatherForecast.Include(a => a.Location).ToListAsync();
        }
        public async Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            // Find the existing weather forecast in the database
            var existingForecast = await _context.WeatherForecast.FindAsync(weatherForecast.Id);

            if (existingForecast == null)
            {
                return null;
            }

            // Update the properties of the existing weather forecast with the values from the provided weather forecast
            // Only update properties that are not null in the input weather forecast
            if (weatherForecast.Date != default(DateOnly))
            {
                existingForecast.Date = weatherForecast.Date;
            }
            if (weatherForecast.IsTemperatureCSet)
            {
                existingForecast.TemperatureC = weatherForecast.TemperatureC;
            }
            if (weatherForecast.IsTemperatureFSet)
            {
                existingForecast.TemperatureF = weatherForecast.TemperatureF;
            }
            if (weatherForecast.LocationId != null)
            {
                bool isLocationIdValid = _context.Location.Any(x => x.Id == weatherForecast.LocationId);

                if (!isLocationIdValid)
                    return null;

                else
                    existingForecast.LocationId = weatherForecast.LocationId;

            }
            if (weatherForecast.Summary != null)
            {
                existingForecast.Summary = weatherForecast.Summary;
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the updated weather forecast
            return existingForecast;
        }
        public async Task<bool> DeleteWeatherForecast(Guid Id)
        {
            var weatherForecastObject = await _context.WeatherForecast.FindAsync(Id);
            if (weatherForecastObject == null)
            {
                // Weather forecast with the given ID not found
                return false;
            }

            _context.WeatherForecast.Remove(weatherForecastObject);
            await _context.SaveChangesAsync();

            // Weather forecast deleted successfully
            return true;
        }
        public async Task<WeatherForecast> CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            var existingForecast = await _context.WeatherForecast.FindAsync(weatherForecast.Id);
            if (existingForecast != null)
            {
                // If a forecast with the same ID already exists, return null

                return null;

            }
            if (weatherForecast.LocationId != null)
            {
                var existingLocation = await _context.Location.FindAsync(weatherForecast.LocationId);
                if (existingLocation == null)
                {
                    throw new WeatherForecastConflictException("Invalid LocationId");
                }

            }
            _context.WeatherForecast.Add(weatherForecast);
            await _context.SaveChangesAsync();
            return weatherForecast;

        }

        public async Task<ICollection<WeatherForecast>> GetWeatherForecastByLocationName(string LocationName)
        {
            return await _context.Set<WeatherForecast>().Where((a => a.Location.Name == LocationName))
                .Include(b => b.Location)// Eagerly load the Location entity
                              .ToListAsync();
        }
    }
}
