using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeHomeTest.Server.Domain;
using TakeHomeTest.Server.Interface;

namespace TakeHomeTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _locationService;

        public LocationController(ILogger<LocationController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet()]
        public async Task<IEnumerable<Location>> Get()
        {
            return await _locationService.GetAllLocations();
        }

       
        [HttpPost("Create")]
        public async Task<ActionResult<Location>> Create([FromBody] Location location)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLocation = await _locationService.CreateLocation(location);
            return CreatedAtAction(nameof(Get), new { id = createdLocation.Id }, createdLocation);

        }

        
    }
}
