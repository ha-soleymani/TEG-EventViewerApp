using Domain.Models;

namespace Domain.Interfaces
{
    public interface IVenueProvider
    {
        Task<List<Venue>> GetVenuesAsync();
    }
}
