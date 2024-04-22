using Microsoft.EntityFrameworkCore;
using TakeHomeTest.Server.Domain;
using TakeHomeTest.Server.Interface;

namespace TakeHomeTest.Server.Services
{
    public class LocationService : ILocationService
    {
        private readonly DatabaseContext _context;
        public LocationService(DatabaseContext context)
        {

            _context = context;

        }
        public async Task<Location> CreateLocation(Location location)
        {
            var existingLocation = await _context.Location.FindAsync(location.Id);
            if (existingLocation != null)
            {
                throw new WeatherForecastConflictException("A Location with the same ID already exists");
            }
            
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
            return location;

        }

        public async Task<ICollection<Location>> GetAllLocations()
        {
            return await _context.Location.ToListAsync();
        }
        
    }
}
