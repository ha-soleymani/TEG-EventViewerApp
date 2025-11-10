using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VenueProvider : IVenueProvider
    {
        private readonly HttpClient _http;
        private readonly ILogger<VenueProvider> _logger;
        private readonly ExternalApiOptions _options;
        public VenueProvider(HttpClient http, ILogger<VenueProvider> logger, IOptions<ExternalApiOptions> options)
        {
            _http = http;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<List<Venue>> GetVenuesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching venues from {Endpoint}", _options.EventDtaEndpoint);

                var res = await _http.GetAsync(_options.EventDtaEndpoint);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();

                _logger.LogInformation("Successfully fetched venues data");

                var doc = JsonDocument.Parse(json);
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                return doc.RootElement.GetProperty("venues").Deserialize<List<Venue>>(options) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch venues");
                return [];
            }
        }
    }
}
