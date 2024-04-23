using TakeHomeTest.Server.Domain;

namespace TakeHomeTest.Server.Interface
{
    public interface ILocationService
    {
        Task<ICollection<Location>> GetAllLocations();
        Task<Location> CreateLocation(Location location);
    }
}
