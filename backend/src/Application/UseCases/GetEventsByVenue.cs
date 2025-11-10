using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases;

public class GetEventsByVenue
{
    private readonly IEventDataProvider _provider;
    public GetEventsByVenue(IEventDataProvider provider) => _provider = provider;

    public Task<List<Event>> ExecuteAsync(int venueId) =>
        _provider.GetEventsByVenueIdAsync(venueId);
}