using Domain.Models;

namespace Domain.Interfaces
{
    public interface IEventDataProvider
    {
        Task<List<Event>> GetEventsByVenueIdAsync(int venueId);
    }

}
