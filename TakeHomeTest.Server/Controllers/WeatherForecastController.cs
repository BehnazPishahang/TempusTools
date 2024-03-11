using Microsoft.AspNetCore.Mvc;
using TakeHomeTest.Server.Services;

namespace TakeHomeTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForcastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForcastService = weatherForecastService;
        }

        [HttpGet()]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _weatherForcastService.GetAllWeatherForecasts();
        }

        [HttpPost("Update")]
        public WeatherForecast Update()
        {
            //TODO get data from request body
            //use weatherForcastService to update a WeatherForecast
            return null;
        }

        [HttpPost("Create")]
        public WeatherForecast Create()
        {
            //TODO get data from request body
            // use weatherForcastService to create a WeatherForecast
            return null;
        }

        [HttpDelete("Delete")]
        public bool Delete()
        {
            //TODO get id from query params
            //use weatherForcastService to delete a WeatherForecast
            return false;
        }
    }
}
