using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class GetVenues
    {
        private readonly IVenueProvider _provider;
        public GetVenues(IVenueProvider provider) => _provider = provider;
        public Task<List<Venue>> ExecuteAsync() => _provider.GetVenuesAsync();
    }
}
