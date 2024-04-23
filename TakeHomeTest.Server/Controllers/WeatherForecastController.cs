using Microsoft.AspNetCore.Mvc;
using TakeHomeTest.Server.Domain;
using TakeHomeTest.Server.Interface;
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
        [Route("[action]")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _weatherForcastService.GetAllWeatherForecasts();
        }

        [HttpGet()]
        [Route("[action]")]
        public async Task<IEnumerable<WeatherForecast>> GetByLocationName(string LocationName)
        {
            return await _weatherForcastService.GetWeatherForecastByLocationName(LocationName);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<WeatherForecast>> Update([FromBody] WeatherForecast weatherForecast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call the service method to update the weather forecast
            var updatedWeatherForecast = await _weatherForcastService.UpdateWeatherForecast(weatherForecast);

            // If the update was successful, return the updated weather forecast
            if (updatedWeatherForecast != null)
            {
                return Ok(updatedWeatherForecast);
            }
            else
            {
                return NotFound("Please check the LocationId and weatherForecastId.");
            }
           
        }

        [HttpPost("Create")]
        public async Task<ActionResult<WeatherForecast>> Create([FromBody] WeatherForecast weatherForecast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdWeatherForecast = await _weatherForcastService.CreateWeatherForecast(weatherForecast);
            if (createdWeatherForecast == null)
            {
                return Conflict("A weather forecast with the same ID already exists.");
            }
            return CreatedAtAction(nameof(Get), new { id = createdWeatherForecast.Id }, createdWeatherForecast);


        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var isDeleted = await _weatherForcastService.DeleteWeatherForecast(id);
            return Ok(isDeleted);
        }


    }
}
