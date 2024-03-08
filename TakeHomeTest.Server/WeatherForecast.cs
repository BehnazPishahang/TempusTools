using Microsoft.EntityFrameworkCore;

namespace TakeHomeTest.Server
{
    public class WeatherForecast
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}
